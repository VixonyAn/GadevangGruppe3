using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BaneFolder
{
    public class DeleteBaneModel : PageModel
    {
        IBaneService _baeService;
        [BindProperty]
        public Bane bane { get; set; }
        public DeleteBaneModel(IBaneService baneService)
        {
            _baeService = baneService;
        }
        public async void OnGetAsync(int baneId)
        {
            bane = await _baeService.GetBaneByIdAsync(baneId);
        }

        public async Task<IActionResult> OnPostAsync(int baneId) 
        {
            try 
            {
                _baeService.DeleteBaneAsync(bane.BaneId);
                return RedirectToPage("ShowAllBane");
            }
            catch (Exception ex)
            {
                List<Bane> baner = new List<Bane>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
