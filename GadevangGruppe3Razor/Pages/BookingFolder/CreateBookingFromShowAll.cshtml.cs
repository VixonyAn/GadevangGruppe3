using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadevangGruppe3Razor.Pages.BookingFolder
{
    public class CreateBookingFromShowAllModel : PageModel
    {

        #region Instance Fields
        IBookingService _bookingService;
        IBaneService _baneService;
        IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public Booking Booking { get; set; }
        [BindProperty] public int BaneId { get; set; }
        [BindProperty] public Bruger Bruger1 { get; set; }
        [BindProperty] public Bruger Bruger2 { get; set; }
        [BindProperty] public int Bruger2ID { get; set; }
        [BindProperty] public int StartTid { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> BrugerSelectList { get; set; }

        public string MessageError { get; set; }

        public string GæstMessage { get; set; }

        public string ErrorAntalBookinger { get; set; }
        #endregion

        public CreateBookingFromShowAllModel(IBookingService bookingService, IBaneService baneService, IBrugerService brugerService)
        {
            _bookingService = bookingService;
            _baneService = baneService;
            _brugerService = brugerService;

            CreateBrugerSelectList();
        }

        public async Task<IActionResult> OnGetAsync(int baneId, int startTid,DateOnly dato )
        {
            BaneId = baneId; 
            Booking = new Booking();
            Booking.Dato = DateOnly.FromDateTime(DateTime.Now);
            StartTid = startTid;
            try
            {
                Email = HttpContext.Session.GetString("Email");
                if (Email == null)
                {
                    return RedirectToPage("/BrugerFolder/Login");
                }
                else
                {
                    Bruger1 = await _brugerService.GetBrugerByEmailAsync(Email);
                    Booking.BaneId = baneId;
                }
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        public async void CreateBrugerSelectList()
        {
            BrugerSelectList = new List<SelectListItem>();
            BrugerSelectList.Add(new SelectListItem("Vælg din partner", "-1"));
            foreach (Bruger b in await _brugerService.GetAllBrugerAsync())
            {                
                    SelectListItem selectListItem = new SelectListItem($"Brugernavn: {b.Brugernavn}, Email:{b.Email}", b.BrugerId.ToString());
                    BrugerSelectList.Add(selectListItem);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Email = HttpContext.Session.GetString("Email");
                if (Email == null)
                {
                    return RedirectToPage("/BrugerFolder/Login");
                }
                Bruger1 = await _brugerService.GetBrugerByEmailAsync(Email);
                if (Bruger2ID == Bruger1.BrugerId) 
                {
                    MessageError = "Du kan ikke vælge dig selv som partner";
                    return Page();
                }
              
                Bruger2 = await _brugerService.GetBrugerByIdAsync(Bruger2ID);
                if (Bruger2.MedlemskabsTypen == MedlemskabsType.Passivt_Medlemskab || Bruger2.Verificeret == false)
                {
                    GæstMessage = "Da din partner gælder som en gæst, koster det 50 kr. pr. gæste time";
                }

                DateOnly EndDay = Booking.Dato.AddDays(14);
                List<Booking> EksisterendeBookinger = await _bookingService.GetBookingByBrugerId(Bruger1.BrugerId);

                int antalBookingerIndenfor14Dage = EksisterendeBookinger.Where(b => b.Dato >= Booking.Dato && b.Dato<= EndDay).Count();
                if (antalBookingerIndenfor14Dage >= 4)
                {
                    ErrorAntalBookinger = "Du kan ikke have mere end 4 bookinger indefor en 14 dags periode, fra dags dato";
                    return Page();
                }

                await _bookingService.CreateBookingAsync(new Booking(Booking.BookingId, Booking.BaneId, Booking.Dato, StartTid, Bruger1.BrugerId, Bruger2.BrugerId, Booking.Beskrivelse));

                return RedirectToPage("/BookingFolder/ShowAllBookinger");
            }
            catch (Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
            }
            return Page();
        }
        
    }
}
