using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class CreateModel : PageModel
    {
		private IBrugerService _brugerService;

        [BindProperty]
        public Bruger Bruger { get; set; }

        [BindProperty]
        public int BrugerId { get; set; }

        public CreateModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }
        public void OnGet(int brugerId)
        {
            Bruger = new Bruger();
            BrugerId = brugerId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
				await _brugerService.CreateBrugerAsync(Bruger);
				return RedirectToPage("ShowAllBruger", new { BrugerId = BrugerId });
			}
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("ShowAllBruger", new { BrugerId = BrugerId });
        }
    }
}
