using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
/*
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
        /// Funktion når Update siden bliver indlæst
        /// </summary>
        /// <param name="eventId">Id på Begivenheden der skal opdateres</param>
        /// <returns>Begivenhedens informationer</returns>
        public async Task<IActionResult> OnGetAsync(int eventId)
        {
            Begivenhed = await _begivenhedService.GetBegivenhedByIdAsync(eventId);
            return Page();
        }

        /// <summary>
        /// Funktion når "Opdater" klikkes på Update siden
        /// </summary>
        /// <param name="eventId">Id på Begivenhed der skal opdateres</param>
        /// <returns>
        /// True: Begivenhed bliver opdateret og brugeren sendt tilbage til oversigten
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int eventId)
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                await _begivenhedService.UpdateBegivenhedAsync(new Begivenhed(eventId, Begivenhed.Titel, Begivenhed.Sted, Begivenhed.Dato, Begivenhed.Beskrivelse, Begivenhed.MedlemMax, Begivenhed.Pris), eventId);
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
*/