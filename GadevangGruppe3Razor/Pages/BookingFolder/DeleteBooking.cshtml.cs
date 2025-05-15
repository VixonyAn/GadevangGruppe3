using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BookingFolder
{
    public class DeleteBookingModel : PageModel
    {
        IBookingService _bookingService;
        [BindProperty] public Booking booking { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }

        public DeleteBookingModel(IBookingService bookingService )
        {
            _bookingService = bookingService;
        }

        public async void OnGetAsync(int bookingId)
        {
            booking = await _bookingService.GetBookingByIdAsync(bookingId);
        }

        public async Task<IActionResult> OnPostAsync(int bookingId) 
        {
            if (Confirm == false) 
            {
                MessageError = $"Husk at klikke Bekræft";
                return Page();
            }
            try 
            {
                await _bookingService.DeleteBookingAsync(booking.BookingId);
                return RedirectToPage("/BrugerFolder/Profile");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
