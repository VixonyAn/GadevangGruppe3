using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace GadevangGruppe3Razor.Pages.BookingFolder
{
    public class CrateBookingModel : PageModel
    {
        IBookingService _bookingService;
        IBaneService _baneService;
        IBrugerService _brugerService;
        [BindProperty]
        public Booking booking { get; set; }
        [BindProperty]
        public int baneId { get; set; }

        [BindProperty]
        public Bruger bruger1 { get; set; }
        [BindProperty]
        public Bruger bruger2 { get; set; }

        public string Email { get; set; }
        public List<SelectListItem> TidSelectList { get; set; }

        public CrateBookingModel(IBookingService bookingService,IBaneService baneService,IBrugerService brugerService)
        {
            _bookingService = bookingService;
            _baneService = baneService;
            _brugerService = brugerService;

            CreateTidSelectList();
        }
        public async Task<IActionResult> OnGetAsync(int baneId)
        {
            try
            {
                //Email = HttpContext.Session.GetString("Email");
                //if (Email == null)
                //{
                //    return RedirectToPage("/BrugerFolder/Login");
                //}
                //else
                //{
                //    bruger1 = await _brugerService.GetBrugeryEmailAsync(Email);

                //}
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        private void CreateTidSelectList() 
        {
            TidSelectList = new List<SelectListItem>();
            TidSelectList.Add(new SelectListItem("8", ":00-9:00"));
            TidSelectList.Add(new SelectListItem("9", ":00-10:00"));
        }
        public async Task<IActionResult> OnPostAsync(int baneId) 
        {
            return Page();
        }

    }
}
