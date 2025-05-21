using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;

namespace GadevangGruppe3Razor.Pages.TilmeldBegivenhedFolder
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public Bruger CurrentBruger { get; set; }
        [BindProperty] public int EventId { get; set; }
        [BindProperty] public string Kommentar { get; set; }
        public string Email { get; set; }
        #endregion

        #region Constructor
        public CreateModel(ITilmeldBegivenhedService tilmeldBegivenhedService, IBrugerService brugerService)
        {
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion når Create siden bliver indlæst
        /// </summary>
        /// <param name="eventId">Id på Begivenheden man ønsker at tilmelde sig</param>
        /// <returns>Begivenhedens Id, og Brugerens Id</returns>
        public async Task<IActionResult> OnGetAsync(int eventId)
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
                    CurrentBruger = await _brugerService.GetBrugerByEmailAsync(Email);
                    EventId = eventId;
                }
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        /// <summary>
        /// Funktion når "Opret" klikkes på Create siden
        /// </summary>
        /// <returns>
        /// True: Tilmelding bliver oprettet og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            //if (!ModelState.IsValid) { return Page(); } // Poul said we could drop this
            try
            {
                await _tilmeldBegivenhedService.CreateTilmeldBAsync(new TilmeldBegivenhed(CurrentBruger.BrugerId, EventId, Kommentar));
                return RedirectToPage("/BrugerFolder/Profile");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
        #endregion
    }
}
