using PetGrubBakcend.DTOs;
using Razorpay.Api;

namespace PetGrubBakcend.Services.Razorpay
{
    public interface IRazorPayService
    {
        Task<string> CreatePaymentAsync(long price);
        Task<bool> verifyPaymentSignatureAsync(PaymentDto paymentDto);
    }
}
