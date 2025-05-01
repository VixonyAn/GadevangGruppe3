using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Services;
using GadevangGruppe3Razor.Models;
using Microsoft.Extensions.Hosting;

namespace GadevangGruppe3Razor.Pages.BegivenhedFolder
{
    public class ShowAllBegivenhedModel : PageModel
    {
        #region Instance Fields
        private IBegivenhedService _begivenhedService;
        #endregion

        #region Properties
        public List<Begivenhed> Begivenheder { get; set; }
        #endregion

        #region Constructor
        public ShowAllBegivenhedModel(IBegivenhedService begivenhedService)
        { // etablerer forbindelse til interface - dependency injection
            _begivenhedService = begivenhedService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync() // OnGet kører når siden indlæses
        { // await låser den del af applikationen som afhænger af dataen der ventes på, mens resten af programmet kan blive ved med at køre
            try
            {
                Begivenheder = await _begivenhedService.GetAllBegivenhedAsync(); // fylder listen med data
            }
            catch (Exception ex)
            {
                Begivenheder = new List<Begivenhed>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
        #endregion
    }
}
