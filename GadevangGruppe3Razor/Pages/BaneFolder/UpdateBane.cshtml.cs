using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadevangGruppe3Razor.Pages.BaneFolder1
{
    public class UpdateBaneModel : PageModel
    {
        IBaneService _baneService;

        [BindProperty]
        public Bane bane { get; set; }

        public List<SelectListItem> TypeSelectList { get; set; }

        public List<SelectListItem> Milj�SelectList { get; set; }

        public UpdateBaneModel(IBaneService baneservice)
        {
            _baneService = baneservice;
            CreateBaneTypeSelectList();
            CreateBaneMilj�SelectList();
        }

        public async void CreateBaneTypeSelectList()
        {
            TypeSelectList = new List<SelectListItem>();
            TypeSelectList.Add(new SelectListItem("V�lg en type", "-1"));
            TypeSelectList.Add(new SelectListItem("Tennis", "Tennis"));
            TypeSelectList.Add(new SelectListItem("Paddel", "Paddel"));


        }

        public void CreateBaneMilj�SelectList()
        {
            Milj�SelectList = new List<SelectListItem>();
            Milj�SelectList.Add(new SelectListItem("V�lg Milj�", "-1"));
            Milj�SelectList.Add(new SelectListItem("Udend�rs", "Udend�rs"));
            Milj�SelectList.Add(new SelectListItem("Indend�rs", "Indend�rs"));
        }

        public async Task<IActionResult> OnGetAsync(int baneId)
        {
            bane= await _baneService.GetBaneByIdAsync(baneId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int baneId)
        {
            if (!ModelState.IsValid) { return Page(); }
            try 
            {
                _baneService.UpdateBaneAsync(baneId, new Bane(baneId, bane.Type, bane.Milj�, bane.Beskrivelse));
            }
            catch (Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
            }
            return RedirectToPage("ShowAllBane");
        }
    }
}
