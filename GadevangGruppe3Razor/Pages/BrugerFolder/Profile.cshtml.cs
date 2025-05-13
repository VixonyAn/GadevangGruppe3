using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class ProfileModel : PageModel
    {
        #region Instance Fields
        private IBrugerService _brugerService;
        private ITilmeldBegivenhedService _tilmeldBegivenhedService;
        #endregion

        #region Properties
        [BindProperty] public Bruger ProfileBruger { get; set; }
        public List<TilmeldBegivenhed> TilmeldBList { get; set; }
        [BindProperty] public string Email { get; set; }
        #endregion

        #region Constructor
        public ProfileModel(IBrugerService brugerService, ITilmeldBegivenhedService tilmeldBegivenhedService)
        {
            _tilmeldBegivenhedService = tilmeldBegivenhedService;
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
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
                    ProfileBruger = await _brugerService.GetBrugerByEmailAsync(Email);
                    TilmeldBList = await _tilmeldBegivenhedService.GetTilmeldBByBrugerIdAsync(ProfileBruger.BrugerId);
                }
            }
            catch (Exception ex)
            {
                ProfileBruger = new Bruger();
                TilmeldBList = new List<TilmeldBegivenhed>();
                ViewData["Title"] = ex.Message;
            }
            return Page();
		}
        #endregion
    }
}
