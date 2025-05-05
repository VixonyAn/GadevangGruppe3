using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BegivenhedFolder
{
    public class UpdateModel : PageModel
    {
        #region Instance Fields
        private IBegivenhedService _begivenhedService;
        #endregion

        #region Properties
        [BindProperty] public Begivenhed Begivenhed { get; set; }
        #endregion

        #region Constructor
        public UpdateModel(IBegivenhedService begivenhedService)
        {
            _begivenhedService = begivenhedService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion n�r Update siden bliver indl�st
        /// </summary>
        /// <param name="EventId">Id p� Begivenheden der skal opdateres</param>
        /// <returns>Begivenhedens informationer</returns>
        public async Task<IActionResult> OnGetAsync(int EventId)
        {
            Begivenhed = await _begivenhedService.GetBegivenhedFromIdAsync(EventId);
            return Page();
        }

        /// <summary>
        /// Funktion n�r "Opdater" klikkes p� Update siden
        /// </summary>
        /// <param name="EventId">Id p� Begivenhed der skal opdateres</param>
        /// <returns>
        /// True: Begivenhed bliver opdateret og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindl�ses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int EventId)
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                await _begivenhedService.UpdateBegivenhedAsync(new Begivenhed(EventId, Begivenhed.Titel, Begivenhed.Sted, Begivenhed.Dato, Begivenhed.Beskrivelse, Begivenhed.MedlemMax, Begivenhed.Pris), EventId);
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
