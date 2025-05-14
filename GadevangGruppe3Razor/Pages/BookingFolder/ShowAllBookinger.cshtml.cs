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
        public Dictionary<int,string> Tider { get; set; }

        public List<Booking> Bookinger { get; set; }
        
        public string NotVerafiedMessage { get; set; }

        public ShowAllBookingerModel(IBookingService bookingService,IBaneService baneservice,IBrugerService brugerService)
        {
            _bookingservice = bookingService;
            _baneService = baneservice;
            _brugerService = brugerService;

            GenererTider();
        }

        public async Task OnGetAsync()
        {
            Baner = await _baneService.GetAllBaneAsync();
            ValgtDato = DateOnly.FromDateTime(DateTime.Now);
            Bookinger = await _bookingservice.GetBookingByDatoAsync(ValgtDato);

        }

        public void GenererTider()
        {
            Tider = new Dictionary<int, string>();
            Tider.Add(8, "8:00-9:00");
            Tider.Add(9, "9:00-10:00");
            Tider.Add(10, "10:00-11:00");
            Tider.Add(11, "11:00-12:00");
            Tider.Add(12, "12:00-13:00");
            Tider.Add(13, "13:00-14:00");
            Tider.Add(14, "14:00-15:00");
            Tider.Add(15, "15:00-16:00");
            Tider.Add(16, "16:00-17:00");
            Tider.Add(17, "17:00-18:00");
            Tider.Add(18, "18:00-19:00");
            Tider.Add(19, "19:00-20:00");
            Tider.Add(20, "20:00-21:00");
        }

    }
}
