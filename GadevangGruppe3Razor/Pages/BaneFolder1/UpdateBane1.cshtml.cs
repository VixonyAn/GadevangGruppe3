using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BaneFolder1
{
    public class UpdateBane1Model : PageModel
    {
        IBaneService _baneService;

        [BindProperty]
        public Bane bane { get; set; }
        public UpdateBane1Model(IBaneService baneservice)
        {
            _baneService = baneservice;
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
                _baneService.UpdateBaneAsync(baneId, new Bane(baneId, bane.Type, bane.Miljø, bane.Beskrivelse));
            }
            catch (Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
            }
            return RedirectToPage("ShowAllBane1");
        }
    }
}
