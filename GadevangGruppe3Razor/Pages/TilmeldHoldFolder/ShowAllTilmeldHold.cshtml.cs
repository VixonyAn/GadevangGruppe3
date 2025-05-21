using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadevangGruppe3Razor.Pages.TilmeldHoldFolder
{
    public class ShowAllTilmeldHoldModel : PageModel
    {
        #region Instance Fields
        private ITilmeldHoldService _tilmeldHoldService;
        private IBrugerService _brugerService;
        private IHoldService _holdService;
        #endregion

        #region Properties
        public List<TilmeldHold> TilmeldHList { get; set; }
        public List<SelectListItem> HoldSelectList { get; set; }
        [BindProperty(SupportsGet = true)] public string FilterHoldId { get; set; }
        [BindProperty] Hold Hold { get; set; }
        [BindProperty] Bruger Bruger { get; set; }
        public string ConfirmRemoved { get; set; }
        #endregion

        #region Constructor
        public ShowAllTilmeldHoldModel(ITilmeldHoldService tilmeldHoldService, IBrugerService brugerService, IHoldService holdService)
        { // etablerer forbindelse til interface - dependency injection
            _tilmeldHoldService = tilmeldHoldService;
            _brugerService = brugerService;
            _holdService = holdService;
            CreateHoldSelectList();
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync(int BrugerId, int HoldId) // OnGet k�rer n�r siden indl�ses
        { // await l�ser den del af applikationen som afh�nger af dataen der ventes p�, mens resten af programmet kan blive ved med at k�re
            try
            {
                if (FilterHoldId != null)
                {
                    if (FilterHoldId == "-1")
                    {
                        TilmeldHList = await _tilmeldHoldService.GetAllTilmeldHAsync();
                        return Page();
                    }
                    TilmeldHList = await _tilmeldHoldService.GetTilmeldHByHoldIdAsync(Convert.ToInt32(FilterHoldId));
                }
                else
                {
                    if (BrugerId != 0 && HoldId != 0)
                    {
                        Bruger = await _brugerService.GetBrugerByIdAsync(BrugerId);
                        Hold = await _holdService.GetHoldByIdAsync(HoldId);
                        ConfirmRemoved = $"Du har nu frameldt {Bruger.Brugernavn} fra holdet {Hold.Holdnavn}";
                    }
                    TilmeldHList = await _tilmeldHoldService.GetAllTilmeldHAsync();
                }
            }
            catch (Exception ex)
            {
                TilmeldHList = new List<TilmeldHold>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        public async Task CreateHoldSelectList()
        {
            HoldSelectList = new List<SelectListItem>();
            HoldSelectList.Add(new SelectListItem("V�lg hold", "-1"));
            foreach (Hold h in await _holdService.GetAllHoldAsync())
            {
                SelectListItem selectListItem = new SelectListItem($"Hold: {h.Holdnavn}", h.HoldId.ToString());
                HoldSelectList.Add(selectListItem);
            }
        }
        #endregion
    }
}
