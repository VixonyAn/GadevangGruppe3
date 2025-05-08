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
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public Bruger CurrentBruger { get; set; }
        [BindProperty] public TilmeldBegivenhed TilmeldB { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }
        public string Email { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(ITilmeldBegivenhedService tilmeldBegivenhedService, IBrugerService brugerService)
        {
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion når Delete siden bliver indlæst
        /// </summary>
        /// <param name="eventId">Id på Begivenheden man vil frameldes</param>
        /// <returns>Tilmeldingens informationer</returns>
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
                    TilmeldB = await _tilmeldBegivenhedService.GetTilmeldBByIdAsync(CurrentBruger.BrugerId, eventId);
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
        /// Funktion når "Slet" klikkes på Delete siden
        /// </summary>
        /// <param name="eventId">Id på Begivenheden der skal slettes</param>
        /// <returns>
        /// True: Begivenhed bliver slettet og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int eventId)
        {
            if (Confirm == false)
            {
                MessageError = $"Husk at klikke Bekræft";
                return Page();
            }
            try
            {
                await _tilmeldBegivenhedService.DeleteTilmeldBAsync(CurrentBruger.BrugerId, eventId);
                return RedirectToPage("ShowAllBegivenhed");
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
