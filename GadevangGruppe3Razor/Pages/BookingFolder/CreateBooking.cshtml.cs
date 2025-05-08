using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace GadevangGruppe3Razor.Pages.BookingFolder
{
    public class CreateBookingModel : PageModel
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
        
        [BindProperty]
        public int StartTid { get; set; }

        public string Email { get; set; }
        public List<SelectListItem> TidSelectList { get; set; }
        public List<SelectListItem> BrugerSelectList { get; set; }

        public CreateBookingModel(IBookingService bookingService,IBaneService baneService,IBrugerService brugerService)
        {
            _bookingService = bookingService;
            _baneService = baneService;
            _brugerService = brugerService;

            CreateTidSelectList();
            CreateBrugerSelectList();
        }
        public async Task<IActionResult> OnGetAsync(int baneId)
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
                    bruger1 = await _brugerService.GetBrugerByEmailAsync(Email);

                }
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
            TidSelectList.Add(new SelectListItem("10", ":00-11:00"));
            TidSelectList.Add(new SelectListItem("11", ":00-12:00"));
            TidSelectList.Add(new SelectListItem("12", ":00-13:00"));
            TidSelectList.Add(new SelectListItem("13", ":00-14:00"));
            TidSelectList.Add(new SelectListItem("14", ":00-15:00"));
            TidSelectList.Add(new SelectListItem("15", ":00-16:00"));
            TidSelectList.Add(new SelectListItem("16", ":00-17:00"));
            TidSelectList.Add(new SelectListItem("17", ":00-18:00"));
            TidSelectList.Add(new SelectListItem("18", ":00-19:00"));
            TidSelectList.Add(new SelectListItem("19", ":00-20:00"));
            TidSelectList.Add(new SelectListItem("20", ":00-21:00"));
        }

        public async void CreateBrugerSelectList() 
        {
            BrugerSelectList = new List<SelectListItem>();
            BrugerSelectList.Add(new SelectListItem("Vælg din partner", "-1"));
            foreach (Bruger b in await _brugerService.GetAllBrugerAsync()) 
            {
                SelectListItem selectListItem = new SelectListItem ($"Brugernavn: {b.Brugernavn}, Email:{b.Email}", b.BrugerId.ToString());
                BrugerSelectList.Add(selectListItem);
            }
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            try
            {
                await _bookingService.CreateBookingAsync(new Booking(booking.BookingId,baneId,booking.Dato,StartTid,bruger1.BrugerId,bruger2.BrugerId,booking.Beskrivelse));
                return RedirectToPage("ShowAllBane");
            }
            catch (Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
            }
            return Page();
        }

    }
}
