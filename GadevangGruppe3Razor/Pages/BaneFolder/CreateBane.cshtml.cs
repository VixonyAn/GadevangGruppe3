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

        public List<SelectListItem> MiljøSelectList { get; set; }

        public CreateBaneModel(IBaneService baneService)
        {
            _baneService = baneService;
            CreateBaneTypeSelectList();
            CreateBaneMiljøSelectList();
        }

        public void OnGet()
        {
        }

        public async void CreateBaneTypeSelectList() 
        {
            TypeSelectList = new List<SelectListItem>();
            TypeSelectList.Add( new SelectListItem("Vælg en type", "-1"));
            TypeSelectList.Add(new SelectListItem("Tennis", "Tennis"));
            TypeSelectList.Add(new SelectListItem("Paddel", "Paddel"));


        }

        public void CreateBaneMiljøSelectList() 
        {
            MiljøSelectList = new List<SelectListItem>();
            MiljøSelectList.Add(new SelectListItem("Vælg Miljø", "-1"));
            MiljøSelectList.Add(new SelectListItem("Udendørs", "Udendørs"));
            MiljøSelectList.Add(new SelectListItem("Indendørs", "Indendørs"));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                 _baneService.CreateBaneAsync(new Bane(bane.BaneId, bane.Type, bane.Miljø, bane.Beskrivelse));
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
