using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.TilmeldBegivenhedFolder
{
    public class UpdateModel : PageModel
    {
        #region Instance Fields
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        #endregion

        #region Properties
        [BindProperty] public TilmeldBegivenhed TilmeldB { get; set; }
        #endregion

        #region Constructor
        public UpdateModel(ITilmeldBegivenhedService tilmeldBegivenhedService)
        {
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion når Update siden bliver indlæst
        /// </summary>
        /// <param name="brugerId">Id af Brugeren på Tilmeldingen der skal opdateres</param>
        /// <param name="eventId">Id af Begivenheden på Tilmeldingen der skal opdateres</param>
        /// <returns>Tilmeldingens informationer</returns>
        public async Task<IActionResult> OnGetAsync(int brugerId, int eventId)
        {
            TilmeldB = await _tilmeldBegivenhedService.GetTilmeldBByIdAsync(brugerId, eventId);
            return Page();
        }

        /// <summary>
        /// Funktion når "Opdater" klikkes på Update siden
        /// </summary>
        /// <param name="brugerId">Id af Brugeren på Tilmeldingen der skal opdateres</param>
        /// <param name="eventId">Id af Begivenheden på Tilmeldingen der skal opdateres</param>
        /// <param name="kommentar">Kommentar på Tilmeldingen der skal opdateres</param>
        /// <returns>
        /// True: Begivenhed bliver opdateret og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            //if (!ModelState.IsValid) { return Page(); }
            try
            {
                await _tilmeldBegivenhedService.UpdateTilmeldBAsync(TilmeldB.BrugerId, TilmeldB.EventId, TilmeldB.Kommentar);
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
