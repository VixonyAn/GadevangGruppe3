using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class DeleteModel : PageModel
    {
		#region Instance Field
		private IBrugerService _brugerService;
		#endregion

		#region Properties
		[BindProperty] public Bruger Bruger { get; set; }
        [BindProperty] public int BrugerId { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }
		#endregion

		#region Constructor
		public DeleteModel(IBrugerService brugerService)
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
            BrugerId = brugerId;
            Bruger = await _brugerService.GetBrugerByIdAsync(BrugerId);
            return Page();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (Confirm == false)
            {
                MessageError = $"Husk at klikke Bekr�ft";
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
		#endregion
	}
}