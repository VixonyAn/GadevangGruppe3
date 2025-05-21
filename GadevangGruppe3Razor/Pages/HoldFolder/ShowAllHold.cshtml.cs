using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using GadevangGruppe3Razor.Services;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class ShowAllHoldModel : PageModel
    {
        #region Instance Fields
        private IHoldService _holdService;
        private IBrugerService _brugerService;
        private ITilmeldHoldService _tilmeldHoldService;
        #endregion

        #region Properties
        [BindProperty] public List<Hold> HoldListe { get; set; }
		public List<TilmeldHold> Tilmeldinger { get; set; }
		[BindProperty] public Hold Hold { get; set; }
        [BindProperty] public Bruger CurrentBruger { get; set; }
        public string Email { get; set; }
        #endregion

        #region Constructor
        public ShowAllHoldModel(IHoldService holdService, IBrugerService brugerService, ITilmeldHoldService tilmeldHoldService)
        {
            _holdService = holdService;
            _brugerService = brugerService;
            _tilmeldHoldService = tilmeldHoldService;
        }
        #endregion

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
				Tilmeldinger = await _tilmeldHoldService.GetAllTilmeldHAsync();
			}
			catch (Exception ex)
            {
                HoldListe = new List<Hold>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostTilmeldAsync(int holdId)
        {
            Hold = HoldListe.Find(h => h.HoldId == holdId);
			return RedirectToPage("/TilmeldHoldFolder/Create", new { holdId = holdId });
		}
        public async Task<IActionResult> OnPostDeleteAsync(int holdId)
        {
            return RedirectToPage("Delete", new { holdId = holdId });
        }
    }
}
