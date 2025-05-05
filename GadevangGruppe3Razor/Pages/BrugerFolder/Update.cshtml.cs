using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class UpdateModel : PageModel
    {
        private IBrugerService _brugerService;

        [BindProperty]
        public Bruger Bruger { get; set; }

        public UpdateModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }

        public async Task<IActionResult> OnGetAsync(int brugerId)
        {
            Bruger = await _brugerService.GetBrugerByIdAsync(brugerId);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int brugerId) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _brugerService.UpdateBrugerAsync(brugerId, new Bruger(brugerId, Bruger.Brugernavn, Bruger.Adgangskode, Bruger.Email, Bruger.Telefon, Bruger.Medlemskab, Bruger.Positionen, Bruger.Verificeret));
                return RedirectToPage("ShowAllBruger");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
