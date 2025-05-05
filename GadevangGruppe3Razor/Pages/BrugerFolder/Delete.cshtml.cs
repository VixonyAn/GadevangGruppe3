using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class DeleteModel : PageModel
    {
        private IBrugerService _brugerService;

        [BindProperty]
        public Bruger Bruger { get; set; }

        [BindProperty]
        public int BrugerId { get; set; }

        [BindProperty]
        public bool Confirm { get; set; }

        public string Message { get; set; }

        public DeleteModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }
        public async Task<IActionResult> OnGetAsync(int brugerId)
        {
            BrugerId = brugerId;
            Bruger = await _brugerService.GetBrugerByIdAsync(BrugerId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Confirm == false)
            {
                Message = $"Husk at checke godkendelsesboksen af";
                return Page();
            }
            try
            {
				await _brugerService.DeleteBrugerAsync(BrugerId);
				return RedirectToPage("ShowAllBruger", new { BrugerId = BrugerId });
			}
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
