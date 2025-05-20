using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class DeleteModel : PageModel
    {
        private IHoldService _holdService;

        [BindProperty] public Hold Hold { get; set; }
        [BindProperty] public int HoldId { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }

        public DeleteModel(IHoldService holdService)
        {
            _holdService = holdService;
        }

        public async Task<IActionResult> OnGetAsync(int holdId)
        {
            HoldId = holdId;
            Hold = await _holdService.GetHoldByIdAsync(holdId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int holdId)
        {
            if (Confirm == false)
            {
                MessageError = "Husk at klikke bekræft";
                return Page();
            }
			try
			{
                await _holdService.DeleteHoldAsync(holdId);
                return RedirectToPage("ShowAllHold", new { holdId = holdId });
			}
			catch (Exception ex)
			{
                ViewData["ErrorMessage"] = ex.Message;
			}
			return Page();
		}
    }
}
