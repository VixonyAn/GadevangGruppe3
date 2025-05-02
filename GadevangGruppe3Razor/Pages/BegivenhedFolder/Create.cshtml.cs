using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;

namespace GadevangGruppe3Razor.Pages.BegivenhedFolder
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private IBegivenhedService _begivenhedService;
        #endregion

        #region Properties
        [BindProperty] public Begivenhed Begivenhed { get; set; }
        #endregion

        #region Constructor
        public CreateModel(IBegivenhedService begivenhedService)
        {
            _begivenhedService = begivenhedService;
        }
        #endregion

        #region Methods
        public void OnGet() { }

        /// <summary>
        /// Funktion når "Opret" klikkes på Create siden
        /// </summary>
        /// <returns>
        /// True: Begivenhed bliver oprettet og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                await _begivenhedService.CreateBegivenhedAsync(Begivenhed);
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
