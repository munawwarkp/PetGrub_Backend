using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Address
{
    public class AddressRepository:IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context)
        {
            _context = context; 
        }

        public Task<bool> IsexistingAddress(AddressCreateDto addressCreateDto,int userId)
        {
            return _context.Addresses.AnyAsync(a =>
                a.HouseNumber == addressCreateDto.HouseNumber &&
                a.Area == addressCreateDto.Area &&
                a.City == addressCreateDto.City &&
                a.PinCode == addressCreateDto.PinCode &&
                a.State == addressCreateDto.State &&
                a.UserId == userId
                );
        }

        public async Task CreateAddress(AddressUser address)
        {
              _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AddressUser>> GetUserAddresses()
        {
            var addresses = await _context.Addresses.Include(a => a.User).ToListAsync();
            await _context.SaveChangesAsync();
            return addresses;
        }

        public async Task<AddressUser?> GetAddressbyId(int addressId,int userId)
        {
            var addressById = await  _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId);
            return addressById;

        }

        public async Task<bool> DeleteAddress(AddressUser address)
        {
             _context.Addresses.Remove(address);
             var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;

        }

    }
}
