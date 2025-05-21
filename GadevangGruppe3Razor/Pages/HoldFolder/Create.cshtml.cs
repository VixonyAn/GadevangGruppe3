using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private IHoldService _holdService;
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public Hold Hold { get; set; }
        [BindProperty] public int HoldId { get; set; }
        [BindProperty] public List<Bruger> Brugere { get; set; }
        public string MessageError { get; set; }
        public string MessageInvalidMaxMedlemstal { get; set; }
        #endregion

        #region Constructor
        public CreateModel(IHoldService holdService, IBrugerService brugerService)
        {
            _holdService = holdService;
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
        public void OnGet(int holdId)
        {
            Hold = new Hold();
            HoldId = holdId;
			Hold.StartDato= DateOnly.FromDateTime(DateTime.Now);
            Hold.SlutDato= DateOnly.FromDateTime(DateTime.Now);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
				await _holdService.CreateHoldAsync(Hold);
				Brugere = await _brugerService.GetAllBrugerAsync();
				Bruger? instruktør = Brugere.Find(b => b.Brugernavn == Hold.Instruktørnavn);
				if (Hold.MaxMedlemstal >= 5 && Hold.MaxMedlemstal <= 9)
                {
					if (instruktør != null)
					{
						return RedirectToPage("ShowAllHold", new { HoldId = HoldId });
					}
					else
					{
						MessageError = "Den angivede holdinstruktør blev ikke fundet i systemet";
						return Page();
					}
				}
				else
                {
                    MessageInvalidMaxMedlemstal = "Det angivede maximale medlemstal er uden for det lovlige interval";
                    return Page();
				}
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("ShowAllHold", new { HoldId = HoldId });
        }
        #endregion
    }
}
