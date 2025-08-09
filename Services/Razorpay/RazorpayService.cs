using System.Text;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using Razorpay.Api;

namespace PetGrubBakcend.Services.Razorpay
{
    public class RazorpayService:IRazorPayService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public RazorpayService(AppDbContext appDbContext,IConfiguration configuration)
        {
            _context = appDbContext;
            _configuration = configuration;
        }

        //create razorpay order with the given amout
        public async Task<string> CreatePaymentAsync(long price)
        {
            try
            {
                Dictionary<string,object> orderOptions = new Dictionary<string,object>();

                Random random = new Random();
                string TransactionId = random.Next(0, 1000).ToString();

                orderOptions.Add("amount", Convert.ToDecimal(price) * 100);
                orderOptions.Add("currency", "INR");
                orderOptions.Add("receipt", TransactionId);


                string key = _configuration["Razorpay:KeyId"]?? throw new ArgumentNullException("Key id not found in config");
                string secret = _configuration["Razorpay:KeySecret"]?? throw new ArgumentNullException("secret not found in config");


                RazorpayClient client = new RazorpayClient(key, secret);
                Order order = client.Order.Create(orderOptions);

                var razorpayOrderId = order["id"].ToString();
                return razorpayOrderId;


            }
            catch (Exception ex)
            {
                throw new Exception($"failed to create razorpay order : {ex.Message}");
            }
        }


        //verify the payment signature returned by razorpay after checkout


        public async Task<bool> verifyPaymentSignatureAsync(PaymentDto payment)
        {
            if (payment == null ||
                string.IsNullOrWhiteSpace(payment.razorpay_payment_id) ||
                string.IsNullOrWhiteSpace(payment.razorpay_order_id) ||
                string.IsNullOrWhiteSpace(payment.razorpay_signature))
                return false;

            try
            {
                string secret = _configuration["Razorpay:KeySecret"]; // FIXED key name
                string payload = $"{payment.razorpay_order_id}|{payment.razorpay_payment_id}";

                using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secret));
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                string generatedSignature = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                return generatedSignature == payment.razorpay_signature;
            }
            catch
            {
                return false;
            }
        }




        //public async Task<bool> verifyPaymentSignatureAsync(PaymentDto payment)
        //{
        //    if (payment == null ||
        //       string.IsNullOrEmpty(payment.razorpay_payment_id) ||
        //       string.IsNullOrEmpty(payment.razorpay_order_id) ||
        //       string.IsNullOrEmpty(payment.razorpay_signature))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        var attributes = new Dictionary<string, string>()
        //        {
        //             { "razorpay_payment_id", payment.razorpay_payment_id },
        //             { "razorpay_order_id", payment.razorpay_order_id },
        //             { "razorpay_signature", payment.razorpay_signature }
        //        };

        //        Utils.verifyPaymentLinkSignature(attributes);
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}

    }

}
