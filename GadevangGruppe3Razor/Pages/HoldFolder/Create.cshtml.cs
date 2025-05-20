using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class CreateModel : PageModel
    {
        private IHoldService _holdService;

        [BindProperty] public Hold Hold { get; set; }
        [BindProperty] public int HoldId { get; set; }
        public string MessageError { get; set; }
        
        public CreateModel(IHoldService holdService)
        {
            _holdService = holdService;
        }

        public void OnGet(int holdId)
        {
            Hold = new Hold();
            HoldId = holdId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _holdService.CreateHoldAsync(Hold);
                return RedirectToPage("ShowAllHold", new { HoldId = HoldId });
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("ShowAllHold", new { HoldId = HoldId });
        }
    }
}
