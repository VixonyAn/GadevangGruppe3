using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class ProfileModel : PageModel
    {
        #region Instance Fields
        private IHoldService _holdService;
        private IBrugerService _brugerService;
        private IBookingService _bookingService;
        private IBegivenhedService _begivenhedService;
        private ITilmeldHoldService _tilmeldHoldService;
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        #endregion

        #region Properties
        
        public List<TilmeldBegivenhed> TilmeldBList { get; set; }
        public List<TilmeldHold> TilmeldHList { get; set; }
        public List<Booking> BookingList { get; set; }
		[BindProperty] public Bruger CurrentBruger { get; set; }
		[BindProperty] public string Email { get; set; }
        [BindProperty] public Begivenhed Begivenhed { get; set; }
        [BindProperty] public Hold Hold { get; set; }
        public string ConfirmRemoved { get; set; }
        #endregion

        #region Constructor
        public ProfileModel(IHoldService holdService, IBrugerService brugerService, IBookingService bookingService, IBegivenhedService begivenhedService, ITilmeldHoldService tilmeldHoldService, ITilmeldBegivenhedService tilmeldBegivenhedService)
        {
            _holdService = holdService;
            _brugerService = brugerService;
            _bookingService = bookingService;
            _begivenhedService = begivenhedService;
            _tilmeldHoldService = tilmeldHoldService;
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync(int EventId)
        {
            try
            {
				Email = HttpContext.Session.GetString("Email");
				if (Email == null)
				{
					return RedirectToPage("/BrugerFolder/Login");
				}
                else
                {
                    if (EventId != 0)
                    {
                        Begivenhed = await _begivenhedService.GetBegivenhedByIdAsync(EventId);
                        ConfirmRemoved = $"Du har nu meldt adbud til {Begivenhed.Titel}";
                    }
                    CurrentBruger = await _brugerService.GetBrugerByEmailAsync(Email);
                    BookingList = await _bookingService.GetBookingByBrugerId(CurrentBruger.BrugerId);
                    TilmeldHList = await _tilmeldHoldService.GetTilmeldHByBrugerIdAsync(CurrentBruger.BrugerId);
                    TilmeldBList = await _tilmeldBegivenhedService.GetTilmeldBByBrugerIdAsync(CurrentBruger.BrugerId);
                }
            }
            catch (Exception ex)
            {
                CurrentBruger = new Bruger();
                BookingList = new List<Booking>();
                TilmeldHList = new List<TilmeldHold>();
                TilmeldBList = new List<TilmeldBegivenhed>();
                ViewData["Title"] = ex.Message;
            }
            return Page();
		}

        public async Task<IActionResult> OnPostBrugerUpdate(int brugerId)
        {
            return RedirectToPage("BrugerUpdate", new { brugerId = brugerId });
        }
        #endregion
    }
}
