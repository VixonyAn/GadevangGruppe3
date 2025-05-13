using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class UpdateModel : PageModel
    {
        private IBrugerService _brugerService;

        [BindProperty] public Bruger Bruger { get; set; }
        [BindProperty] public int BrugerId { get; set; }
        [BindProperty] public string BilledUrl { get; set; }
        [BindProperty(SupportsGet = true)] public string SelectMedlemskabsType { get; set; }
        [BindProperty(SupportsGet = true)] public string SelectPosition { get; set; }
		public bool SelectCheck { get; set; }
        public string MessageError { get; set; }

        public UpdateModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }

        public async Task<IActionResult> OnGetAsync(int brugerId)
        {
            Bruger = await _brugerService.GetBrugerByIdAsync(brugerId);
            BrugerId = brugerId;
            BilledUrl = Bruger.BilledUrl;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int brugerId) 
        {
			try
			{
				SelectBrugerMedlemskab();
				SelectBrugerPosition();
				if (!SelectCheck)
				{
					MessageError = "Du mangler at vælge en mulighed";
					return Page();
				}
				await _brugerService.UpdateBrugerAsync(brugerId, new Bruger(brugerId, Bruger.Brugernavn, Bruger.Adgangskode, Bruger.Email, Bruger.Telefon, BilledUrl, Bruger.Medlemskab, Bruger.Positionen, Bruger.Verificeret));
				return RedirectToPage("ShowAllBruger", new { BrugerId = BrugerId });
			}
			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = ex.Message;
			}
			return Page();
        }

		private void SelectBrugerMedlemskab()
		{
			if (!SelectMedlemskabsType.IsNullOrEmpty() && SelectMedlemskabsType != "Select")
			{
				MedlemskabsType criteriaMedlemskab = (MedlemskabsType)Enum.Parse(typeof(MedlemskabsType), SelectMedlemskabsType);
				Bruger.Medlemskab = criteriaMedlemskab;
				SelectCheck = true;
			}
			else
			{
				SelectCheck = false;
			}
		}

		private void SelectBrugerPosition()
		{
			if (!SelectPosition.IsNullOrEmpty() && SelectPosition != "Select")
			{
				Position criteriaPosition = (Position)Enum.Parse(typeof(Position), SelectPosition);
				Bruger.Positionen = criteriaPosition;
				SelectCheck = true;
			}
			else
			{
				SelectCheck = false;
			}
		}
	}
}
