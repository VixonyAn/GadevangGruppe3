using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBookingService
    {
        List<Booking> GetAll();
        Booking GetBookingById(int bookingId);

        bool CreateBooking(Booking booking);

        bool UpdateBooking(int bookingId,Booking booking);

        Booking DeleteBooking(int bookingId);
    }
}
