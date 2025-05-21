using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.HoldFolder
{
    public class CreateModel : PageModel
    {
        private IHoldService _holdService;
        private IBrugerService _brugerService;

        [BindProperty] public Hold Hold { get; set; }
        [BindProperty] public int HoldId { get; set; }
        public string MessageError { get; set; }

        public CreateModel(IHoldService holdService, IBrugerService brugerService)
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
				List<Bruger> brugerListe = await _brugerService.GetAllBrugerAsync();
				Bruger? instrukt�r = brugerListe.Find(b => b.Brugernavn == Hold.Instrukt�rnavn);
                if (instrukt�r != null)
                {
                    //if (!Hold.TilmeldteBrugere.Contains(instrukt�r))
                    //               {
                    //                   Hold.TilmeldteBrugere.Add(instrukt�r);
                    //               }
                    return RedirectToPage("ShowAllHold", new { HoldId = HoldId });
				}
                else
                {
                    MessageError = "Den angivede holdinstrukt�r blev ikke fundet i systemet";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("ShowAllHold", new { HoldId = HoldId });
        }
    }
}
