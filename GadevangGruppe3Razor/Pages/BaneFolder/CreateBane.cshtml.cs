using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadevangGruppe3Razor.Pages.BaneFolder1
{
    public class CreateBaneModel : PageModel
    {
        IBaneService _baneService;

        [BindProperty]
        public Bane bane { get; set; }

        public List<SelectListItem> TypeSelectList { get; set; }

        public List<SelectListItem> Milj�SelectList { get; set; }

        public CreateBaneModel(IBaneService baneService)
        {
            _baneService = baneService;
            CreateBaneTypeSelectList();
            CreateBaneMilj�SelectList();
        }

        public void OnGet()
        {
        }

        public async void CreateBaneTypeSelectList() 
        {
            TypeSelectList = new List<SelectListItem>();
            TypeSelectList.Add( new SelectListItem("V�lg en type", "-1"));
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                 _baneService.CreateBaneAsync(new Bane(bane.BaneId, bane.Type, bane.Milj�, bane.Beskrivelse));
                return RedirectToPage("ShowAllBane");
            }
            catch(Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
