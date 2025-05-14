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
        private IBrugerService _brugerService;

        public DateOnly ValgtDato { get; set; }

        public Bane ValgtBane { get; set; }

        public int ValgtTId { get; set; }

        public int Email { get; set; }

        public Bruger CurrentBruger { get; set; }

        public List<Bane> Baner { get; set; }
        public List<int> Tider { get; set; }
        
        public string NotVerafiedMessage { get; set; }

        public ShowAllBookingerModel(IBookingService bookingService,IBaneService baneservice,IBrugerService brugerService)
        {
            _bookingservice = bookingService;
            _baneService = baneservice;
            _brugerService = brugerService;
        }

        public async Task OnGetAsync()
        {
            Baner = await _baneService.GetAllBaneAsync();
        }

       
    }
}
