using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BaneFolder1
{
    public class CreateBaneModel : PageModel
    {
        IBaneService _baneService;

        [BindProperty]
        public Bane bane { get; set; }

        public CreateBaneModel(IBaneService baneService)
        {
            _baneService = baneService;
        }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                 _baneService.CreateBaneAsync(new Bane(bane.BaneId, bane.Type, bane.Miljø, bane.Beskrivelse));
                return RedirectToPage("ShowAllBane1");
            }
            catch(Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
