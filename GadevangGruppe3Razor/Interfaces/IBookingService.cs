using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking> GetBookingByIdAsync(int bookingId);

        Task<bool> CreateBookingAsync(Booking booking);

        Task<bool> UpdateBookingAsync(int bookingId,Booking booking);

        Task<Booking> DeleteBookingAsync(int bookingId);
    }
}
