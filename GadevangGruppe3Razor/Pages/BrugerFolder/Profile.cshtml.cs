using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class ProfileModel : PageModel
    {
        #region Instance Fields
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public Bruger ProfileBruger { get; set; }
        [BindProperty] public string Email { get; set; }
        #endregion

        #region Constructor
        public ProfileModel(IBrugerService brugerService)
        {
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
                }
			}
            catch (Exception ex)
            {
                ProfileBruger = new Bruger();
                ViewData["Title"] = ex.Message;
            }
            return Page();
		}
        #endregion
    }
}
