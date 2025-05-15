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
        private IBrugerService _brugerService;
        private IBookingService _bookingService;
        private IBegivenhedService _begivenhedService;
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        #endregion

        #region Properties
        [BindProperty] public Bruger CurrentBruger { get; set; }
        public List<TilmeldBegivenhed> TilmeldBList { get; set; }
        public List<Booking> BookingList { get; set; }
        
        [BindProperty] public string Email { get; set; }
        [BindProperty] public int EventId { get; set; }
        [BindProperty] public Begivenhed Begivenhed { get; set; }
        public string ConfirmRemoved { get; set; }
        
        [BindProperty] public int Bruger2Id { get; set; }
        [BindProperty] public Bruger Bruger2 { get; set; }
        public string GæsteSpil { get; set; }
        #endregion

        #region Constructor
        public ProfileModel(IBrugerService brugerService, IBookingService bookingService, IBegivenhedService begivenhedService, ITilmeldBegivenhedService tilmeldBegivenhedService)
        {
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
            _begivenhedService = begivenhedService;
            _bookingService = bookingService;
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync(int EventId, int Bruger2Id)
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
                    if (Bruger2Id != 0 && Bruger2 != null)
                    {
                        if (Bruger2.Medlemskab == MedlemskabsType.Passivt_Medlemskab || Bruger2.Verificeret == false)
                        {
                            Bruger2 = await _brugerService.GetBrugerByIdAsync(Bruger2Id);
                            GæsteSpil = $"Da din partner gælder som en gæst, koster det 50 kr. pr. gæste time";
                        }
                    }
                    CurrentBruger = await _brugerService.GetBrugerByEmailAsync(Email);
                    TilmeldBList = await _tilmeldBegivenhedService.GetTilmeldBByBrugerIdAsync(CurrentBruger.BrugerId);
                    BookingList = await _bookingService.GetBookingByBrugerId(CurrentBruger.BrugerId);
                }
            }
            catch (Exception ex)
            {
                CurrentBruger = new Bruger();
                TilmeldBList = new List<TilmeldBegivenhed>();
                BookingList = new List<Booking>();
                ViewData["Title"] = ex.Message;
            }
            return Page();
		}
        #endregion
    }
}
