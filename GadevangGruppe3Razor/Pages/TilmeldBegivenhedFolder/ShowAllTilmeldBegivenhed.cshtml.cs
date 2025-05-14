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
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        public List<TilmeldBegivenhed> TilmeldBList { get; set; }
        public List<SelectListItem> EventSelectList { get; set; }
        [BindProperty(SupportsGet = true)] public string FilterEventId { get; set; }
        #endregion

        #region Constructor
        public ShowAllTilmeldBegivenhedModel(ITilmeldBegivenhedService tilmeldBegivenhedService, IBrugerService brugerService, IBegivenhedService begivenhedService)
        { // etablerer forbindelse til interface - dependency injection
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
            _begivenhedService = begivenhedService;
            _brugerService = brugerService;
            CreateEventSelectList();
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync() // OnGet kører når siden indlæses
        { // await låser den del af applikationen som afhænger af dataen der ventes på, mens resten af programmet kan blive ved med at køre
            try
            {
                if (FilterEventId != null)
                {
                    if (FilterEventId == "-1")
                    {
                        TilmeldBList = await _tilmeldBegivenhedService.GetAllTilmeldBAsync();
                        return Page();
                    }
                    TilmeldBList = await _tilmeldBegivenhedService.GetTilmeldBByEventIdAsync(Convert.ToInt32(FilterEventId));
                }
                else
                {
                    TilmeldBList = await _tilmeldBegivenhedService.GetAllTilmeldBAsync();
                }
            }
            catch (Exception ex)
            {
                TilmeldBList = new List<TilmeldBegivenhed>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
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
