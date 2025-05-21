using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class TilmeldModel : PageModel
    {
        private IHoldService _holdService;
        private IBrugerService _brugerService;

        [BindProperty] public Hold Hold { get; set; }
        [BindProperty] public Bruger CurrentBruger { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string Email { get; set; }
        public string MessageError { get; set; }
        public string MessageMaxTeamExceeded { get; set; }

        public TilmeldModel(IHoldService holdService, IBrugerService brugerService)
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
        public async Task<IActionResult> OnPost(int holdId)
        {
            if (Confirm == false)
            {
                MessageError = "Husk at klikke bekræft";
                return Page();
            }
    //        if (Hold.TilmeldteBrugere.Count > Hold.MaxMedlemstal)
    //        {
				//MessageMaxTeamExceeded = "Holdet kan ikke have flere medlemmer";
    //            return Page();
    //        }
            //Hold.TilmeldteBrugere.Add(CurrentBruger);
			return RedirectToPage("ShowAllHold", new { holdId = holdId });
        }
    }
}
