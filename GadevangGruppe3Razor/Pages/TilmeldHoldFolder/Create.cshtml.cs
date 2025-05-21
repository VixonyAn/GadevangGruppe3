using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.TilmeldHoldFolder
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private ITilmeldHoldService _tilmeldHoldService;
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public Bruger CurrentBruger { get; set; }
        [BindProperty] public int HoldId { get; set; }
        public string Email { get; set; }
        #endregion

        #region Constructor
        public CreateModel(ITilmeldHoldService tilmeldHoldService, IBrugerService brugerService)
        {
            _tilmeldHoldService = tilmeldHoldService;
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion når Create siden bliver indlæst
        /// </summary>
        /// <param name="holdId">Id på Holdet man ønsker at tilmelde sig</param>
        /// <returns>Holdets Id, og Brugerens Id</returns>
        public async Task<IActionResult> OnGetAsync(int holdId)
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
                    HoldId = holdId;
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
                await _tilmeldHoldService.CreateTilmeldHAsync(new TilmeldHold(CurrentBruger.BrugerId, HoldId));
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
