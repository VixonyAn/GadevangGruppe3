using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.TilmeldBegivenhedFolder
{
    public class ShowAllTilmeldBegivenhedModel : PageModel
    {
        #region Instance Fields
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        //private IBrugerService _brugerService;
        //private IBegivenhedService _begivenhedService;
        #endregion

        #region Properties
        public List<TilmeldBegivenhed> TilmeldB { get; set; }
        //public Bruger Bruger { get; set; }
        //public Begivenhed Begivenhed { get; set; }
        //[BindProperty] public TilmeldBegivenhed TilmeldBegivenhed { get; set; }
        #endregion

        #region Constructor
        public ShowAllTilmeldBegivenhedModel(ITilmeldBegivenhedService tilmeldBegivenhedService)//, IBrugerService brugerService, IBegivenhedService begivenhedService)
        { // etablerer forbindelse til interface - dependency injection
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
            //_brugerService = brugerService;
            //_begivenhedService = begivenhedService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync() // OnGet kører når siden indlæses
        { // await låser den del af applikationen som afhænger af dataen der ventes på, mens resten af programmet kan blive ved med at køre
            try
            {
                TilmeldB = await _tilmeldBegivenhedService.GetAllTilmeldBAsync(); // fylder listen med data
                //Bruger = await _brugerService.GetBrugerByIdAsync(TilmeldBegivenhed.BrugerId);
                //Begivenhed = await _begivenhedService.GetBegivenhedByIdAsync(TilmeldBegivenhed.EventId);
            }
            catch (Exception ex)
            {
                TilmeldB = new List<TilmeldBegivenhed>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
        #endregion
    }
}
