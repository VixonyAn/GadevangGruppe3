using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class TilmeldModel : PageModel
    {
        private IHoldService _holdService;

        public TilmeldModel(IHoldService holdService)
        {
            _holdService = holdService;
        }
        public Task<IActionResult> OnGetAsync()
        {
            return Page(); 
        }
    }
}
