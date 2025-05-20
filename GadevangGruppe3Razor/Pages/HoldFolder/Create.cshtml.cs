using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class CreateModel : PageModel
    {
        private IHoldService _holdService;

        [BindProperty] Hold Hold { get; set; }
        [BindProperty] int HoldId { get; set; }
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

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    Page();
        //}
    }
}
