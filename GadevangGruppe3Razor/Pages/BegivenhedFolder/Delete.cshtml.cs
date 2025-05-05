using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BegivenhedFolder
{
    public class DeleteModel : PageModel
    {
        #region Instance Fields
        private IBegivenhedService _begivenhedService;
        #endregion

        #region Properties
        [BindProperty] public Begivenhed Begivenhed { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(IBegivenhedService begivenhedService)
        {
            _begivenhedService = begivenhedService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion n�r Delete siden bliver indl�st
        /// </summary>
        /// <param name="EventId">Id p� Begivenheden der skal slettes</param>
        /// <returns>Begivenhedens informationer</returns>
        public async Task<IActionResult> OnGetAsync(int EventId)
        {
            Begivenhed = await _begivenhedService.GetBegivenhedFromIdAsync(EventId);
            return Page();
        }

        /// <summary>
        /// Funktion n�r "Slet" klikkes p� Delete siden
        /// </summary>
        /// <param name="EventId">Id p� Begivenheden der skal slettes</param>
        /// <returns>
        /// True: Begivenhed bliver slettet og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindl�ses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int EventId)
        {
            if (Confirm == false)
            {
                MessageError = $"Husk at klikke Bekr�ft";
                return Page();
            }
            try
            {
                await _begivenhedService.DeleteBegivenhedAsync(EventId);
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
