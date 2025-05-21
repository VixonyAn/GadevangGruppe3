using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class AfmeldModel : PageModel
    {
        private IHoldService _holdService;
        private IBrugerService _brugerService;

        [BindProperty] public Hold Hold { get; set; }
        [BindProperty] public Bruger CurrentBruger { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string Email { get; set; }
        public string MessageError { get; set; }

        public AfmeldModel(IHoldService holdService, IBrugerService brugerService)
        {
            _holdService = holdService;
            _brugerService = brugerService;
        }
        public async Task<IActionResult> OnGetAsync(int holdId)
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
                Hold = await _holdService.GetHoldByIdAsync(holdId);
            }
            catch (Exception ex)
            {
                ViewData["Title"] = ex.Message;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int holdId)
        {
            if (Confirm == null)
            {
                MessageError = "Husk at klikke bekræft";
                return Page();
            }
            Hold.TilmeldteBrugere.Remove(CurrentBruger);
            return RedirectToPage("ShowAllHold", new { holdId = holdId });
        }
    }
}
