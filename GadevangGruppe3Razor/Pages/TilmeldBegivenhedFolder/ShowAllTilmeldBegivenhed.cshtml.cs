using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadevangGruppe3Razor.Pages.TilmeldBegivenhedFolder
{
    public class ShowAllTilmeldBegivenhedModel : PageModel
    {
        #region Instance Fields
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        private IBegivenhedService _begivenhedService;
        #endregion

        #region Properties
        public List<TilmeldBegivenhed> TilmeldBList { get; set; }
        public List<SelectListItem> EventSelectList { get; set; }
		[BindProperty] public int BegivenhedId { get; set; }
        //[BindProperty(SupportsGet = true)] public string Criteria { get; set; }
        #endregion

        #region Constructor
        public ShowAllTilmeldBegivenhedModel(ITilmeldBegivenhedService tilmeldBegivenhedService, IBegivenhedService begivenhedService)
        { // etablerer forbindelse til interface - dependency injection
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
            _begivenhedService = begivenhedService;
            CreateEventSelectList();
        }
        #endregion

        #region Methods
        public async Task OnGetAsync() // OnGet kører når siden indlæses
        { // await låser den del af applikationen som afhænger af dataen der ventes på, mens resten af programmet kan blive ved med at køre
            try
            {
                //TilmeldBList = await _tilmeldBegivenhedService.GetAllTilmeldBAsync(); // fylder listen med data
                /*TilmeldBList = new List<TilmeldBegivenhed>();
                foreach (TilmeldBegivenhed item in await _tilmeldBegivenhedService.GetAllTilmeldBAsync())
                {
                    Bruger = await _brugerService.GetBrugerByIdAsync(item.BrugerId);
                    Begivenhed = await _begivenhedService.GetBegivenhedByIdAsync(item.EventId);
                    TilmeldBegivenhed TB = new TilmeldBegivenhed(Bruger.BrugerId, Begivenhed.EventId, item.Kommentar);
                    TilmeldBList.Add(TB);
                }*/
                List<TilmeldBegivenhed> tilmeldinger = await _tilmeldBegivenhedService.GetTilmeldBByEventIdAsync(BegivenhedId);
                if (tilmeldinger != null)
                {
                    TilmeldBList = await _tilmeldBegivenhedService.GetTilmeldBByEventIdAsync(BegivenhedId);
                }
                else
                {
                    TilmeldBList = await _tilmeldBegivenhedService.GetAllTilmeldBAsync(); // fylder listen med data
                }
            }
            catch (Exception ex)
            {
                TilmeldBList = new List<TilmeldBegivenhed>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }

        public async Task CreateEventSelectList()
        {
            EventSelectList = new List<SelectListItem>();
            EventSelectList.Add(new SelectListItem("Vælg begivenhed", "-1"));
            foreach (Begivenhed b in await _begivenhedService.GetAllBegivenhedAsync())
            {
                SelectListItem selectListItem = new SelectListItem($"Begivenhed: {b.Titel}, Dato: {b.Dato}", b.EventId.ToString());
                EventSelectList.Add(selectListItem);
            }
        }
        #endregion
    }
}
