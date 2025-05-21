using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class ShowAllHoldModel : PageModel
    {
        private IHoldService _holdService;
        private IBrugerService _brugerService;

        [BindProperty] public List<Hold> HoldListe { get; set; }
        [BindProperty] public Hold Hold { get; set; }
        [BindProperty] public Bruger CurrentBruger { get; set; }
        public string Email { get; set; }

        public ShowAllHoldModel(IHoldService holdService, IBrugerService brugerService)
        {
            _holdService = holdService;
            _brugerService = brugerService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Email = HttpContext.Session.GetString("Email");
                if (Email == null)
                {
                    return RedirectToPage("/BrugerFolder/Login");
                }
                else
                {
                    CurrentBruger = await _brugerService.GetBrugerByEmailAsync(Email);
				}
                Hold = new Hold();
                HoldListe = await _holdService.GetAllHoldAsync();
            }
            catch (Exception ex)
            {
                HoldListe = new List<Hold>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostTilmeldAfmeldAsync(int holdId)
        {
            Hold = HoldListe.Find(h => h.HoldId == holdId);
            if (!Hold.TilmeldteBrugere.Contains(CurrentBruger))
            {
				return RedirectToPage("Tilmeld", new { holdId = holdId });
			}
            else
            {
				return RedirectToPage("Afmeld", new { holdId = holdId });
			}
        }
        public async Task<IActionResult> OnPostDeleteAsync(int holdId)
        {
            return RedirectToPage("Delete", new { holdId = holdId });
        }
    }
}
