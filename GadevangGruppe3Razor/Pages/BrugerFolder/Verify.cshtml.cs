using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class VerifyModel : PageModel
    {
		#region Instance Field
		private IBrugerService _brugerService;
		#endregion

		#region Properties
		[BindProperty] public Bruger Bruger { get; set; }
        [BindProperty] public Bruger CurrentBruger { get; set; }
        [BindProperty] public bool Confirm { get; set; }
		public string Email { get; set; }
        public string MessageError { get; set; }
		#endregion

		#region Constructor
		public VerifyModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }
		#endregion

		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="brugerId"></param>
		/// <returns></returns>
		public async Task<IActionResult> OnGetAsync(int brugerId)
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
				Bruger = await _brugerService.GetBrugerByIdAsync(brugerId);
			}
			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = ex.Message;
			}
            return Page();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brugerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int brugerId)
        {
			Bruger = await _brugerService.GetBrugerByIdAsync(brugerId);
			if (Confirm == false)
			{
				MessageError = "Husk at klikke bekræft";
				return Page();
			}
			if (!Bruger.Verificeret)
            {
                Bruger.Verificeret = true;
                await _brugerService.VerifyBrugerAsync(brugerId, Bruger);
				return RedirectToPage("ShowAllBruger", new { brugerId = brugerId });
			}
			else
            {
                Bruger.Verificeret = false;
                await _brugerService.VerifyBrugerAsync(brugerId, Bruger);
                return RedirectToPage("ShowAllBruger", new { brugerId = brugerId });
            }
		}
		#endregion
	}
}
