using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Services;
using GadevangGruppe3Razor.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GadevangGruppe3Razor.Pages.BegivenhedFolder
{
    public class ShowAllBegivenhedModel : PageModel
    {
        #region Instance Fields
        private IBegivenhedService _begivenhedService;
        private IBrugerService _brugerService;
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        #endregion

        #region Properties
        public List<Begivenhed> Begivenheder { get; set; }
        public List<TilmeldBegivenhed> Tilmeldinger { get; set; }
        [BindProperty] public Bruger CurrentBruger { get; set; }
        [BindProperty] public string UnverifiedWarning { get; set; }
        public string Email { get; set; }
        #endregion

        #region Constructor
        public ShowAllBegivenhedModel(IBegivenhedService begivenhedService, IBrugerService brugerService, ITilmeldBegivenhedService tilmeldBegivenhedService)
        { // etablerer forbindelse til interface - dependency injection
            _begivenhedService = begivenhedService;
            _brugerService = brugerService;
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync() // OnGet kører når siden indlæses
        { // await låser den del af applikationen som afhænger af dataen der ventes på, mens resten af programmet kan blive ved med at køre
            try
            {
                Email = HttpContext.Session.GetString("Email");
                if (Email == null)
                {
                    return RedirectToPage("/BrugerFolder/Login");
                }
                else
                {
                    CurrentBruger = await _brugerService.GetBrugerByEmailAsync(Email);
                    if (!CurrentBruger.Verificeret)
				    {
                        UnverifiedWarning = $"Du kan desværre ikke tilmelde dig begivenheder lige nu, da dit medlemskab ikke er verificeret.";
                    }
                    Begivenheder = await _begivenhedService.GetAllBegivenhedAsync(); // fylder listen med data
                    Tilmeldinger = await _tilmeldBegivenhedService.GetAllTilmeldBAsync();
                }
            }
            catch (Exception ex)
            {
                Begivenheder = new List<Begivenhed>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
        #endregion
    }
}
