using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.TilmeldBegivenhedFolder
{
    public class DeleteModel : PageModel
    {
        #region Instance Fields
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        #endregion

        #region Properties
        [BindProperty] public TilmeldBegivenhed TilmeldB { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(ITilmeldBegivenhedService tilmeldBegivenhedService)
        {
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion n�r Delete siden bliver indl�st
        /// </summary>
        /// <param name="brugerId">Id p� Brugeren der vil frameldes</param>
        /// <param name="eventId">Id p� Begivenheden man vil frameldes</param>
        /// <returns>Tilmeldingens informationer</returns>
        public async Task<IActionResult> OnGetAsync(int brugerId, int eventId)
        {
            TilmeldB = await _tilmeldBegivenhedService.GetTilmeldBByIdAsync(brugerId, eventId);
            return Page();
        }

        /// <summary>
        /// Funktion n�r "Slet" klikkes p� Delete siden
        /// </summary>
        /// <param name="brugerId">Id p� Brugeren der skal frameldes</param>
        /// <param name="eventId">Id p� Begivenheden der skal frameldes</param>
        /// <returns>
        /// True: Tilmelding bliver slettet og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindl�ses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int brugerId, int eventId)
        {
            if (Confirm == false)
            {
                MessageError = $"Husk at klikke Bekr�ft";
                return Page();
            }
            try
            {
                await _tilmeldBegivenhedService.DeleteTilmeldBAsync(brugerId, eventId);
                return RedirectToPage("ShowAllTilmeldBegivenhed");
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
