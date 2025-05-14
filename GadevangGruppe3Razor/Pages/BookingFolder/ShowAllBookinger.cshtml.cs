using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BookingFolder
{
    public class ShowAllBookingerModel : PageModel
    {
        private IBookingService _bookingservice;
        private IBaneService _baneService;

        public DateOnly ValgtDato { get; set; }

        public Bane ValgtBane { get; set; }

        public int ValgtTId { get; set; }

        public int Emial { get; set; }

        public int CurentBruger { get; set; }

        public ShowAllBookingerModel(IBookingService bookingService,IBaneService baneservice)
        {
            _bookingservice = bookingService;
            _baneService = baneservice;
        }

        public void OnGet()
        {

        }

       
    }
}
