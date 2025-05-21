using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.TilmeldHoldFolder
{
    public class DeleteModel : PageModel
    {
        #region Instance Fields
        private ITilmeldHoldService _tilmeldHoldService;
        #endregion

        #region Properties
        [BindProperty] public TilmeldHold TilmeldH { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }
        [BindProperty] public int BrugerId { get; set; }
        [BindProperty] public int HoldId { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(ITilmeldHoldService tilmeldHoldService)
        {
            _tilmeldHoldService = tilmeldHoldService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion når Delete siden bliver indlæst
        /// </summary>
        /// <param name="brugerId">Id på Brugeren der vil frameldes</param>
        /// <param name="holdId">Id på Holdet man vil frameldes</param>
        /// <returns>Tilmeldingens informationer</returns>
        public async Task<IActionResult> OnGetAsync(int brugerId, int holdId)
        {
            BrugerId = brugerId;
            HoldId = holdId;
            TilmeldH = await _tilmeldHoldService.GetTilmeldHByIdAsync(brugerId, holdId);
            return Page();
        }

        /// <summary>
        /// Funktion når "Slet" klikkes på Delete siden
        /// </summary>
        /// <param name="brugerId">Id på Brugeren der skal frameldes</param>
        /// <param name="holdId">Id på Holdet der skal frameldes</param>
        /// <returns>
        /// True: Tilmelding bliver slettet og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int brugerId, int holdId)
        {
            if (Confirm == false)
            {
                MessageError = $"Husk at klikke Bekræft";
                return Page();
            }
            try
            {
                await _tilmeldHoldService.DeleteTilmeldHAsync(brugerId, holdId);
                return RedirectToPage("ShowAllTilmeldHold", new { BrugerId = BrugerId, HoldId = HoldId });
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
