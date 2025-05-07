using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class ShowAllBrugerModel : PageModel
    {
        private IBrugerService _brugerService;
        
        public List<Bruger> Brugere { get; set; }

        public ShowAllBrugerModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }
        public async Task OnGetAsync()
        {
            try
            {
                Brugere = await _brugerService.GetAllBrugerAsync();
            }
            catch (Exception ex)
            {
                Brugere = new List<Bruger>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int brugerId)
        {
            return RedirectToPage("Delete", new { brugerId = brugerId });
        }

        public async Task<IActionResult> OnPostUpdateAsync(int brugerId)
        {
            return RedirectToPage("Update", new { brugerId = brugerId });
        }
    }
}
