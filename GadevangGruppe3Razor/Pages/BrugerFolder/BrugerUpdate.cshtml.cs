using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class BrugerUpdateModel : PageModel
    {
		private IBrugerService _brugerService;

		[BindProperty] public Bruger CurrentBruger { get; set; }
		[BindProperty] public int BrugerId { get; set; }
		[BindProperty] public string BilledUrl { get; set; }
		[BindProperty] public string Email { get; set; }
		[BindProperty(SupportsGet = true)] public string SelectKøn { get; set; }
		public bool SelectCheck { get; set; }
		public string MessageError { get; set; }

		public BrugerUpdateModel(IBrugerService brugerService)
		{
			_brugerService = brugerService;
		}

		public async Task<IActionResult> OnGetAsync(int brugerId)
		{
			Email = HttpContext.Session.GetString("Email");
			if (Email == null)
			{
				return RedirectToPage("/BrugerFolder/Login");
			}
			else
			{
				CurrentBruger = await _brugerService.GetBrugerByEmailAsync(Email);
			}
			BrugerId = brugerId;
			BilledUrl = CurrentBruger.BilledUrl;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int brugerId)
		{
			try
			{
				SelectBrugerKøn();
				if (!SelectCheck)
				{
					MessageError = "Du mangler at vælge en mulighed";
					return Page();
				}
				await _brugerService.BrugerUpdateBrugerAsync(brugerId, new Bruger(brugerId, CurrentBruger.Brugernavn, CurrentBruger.Adgangskode, CurrentBruger.Fødselsdato, CurrentBruger.Kønnet, CurrentBruger.Email, CurrentBruger.Telefon, BilledUrl, CurrentBruger.MedlemskabsTypen, CurrentBruger.Positionen, CurrentBruger.Verificeret));
				return RedirectToPage("Profile", new { BrugerId = BrugerId });
			}
			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = ex.Message;
			}
			return Page();
		}

		private void SelectBrugerKøn()
		{
			if (!SelectKøn.IsNullOrEmpty() && SelectKøn != "Select")
			{
				Køn criteriaKøn = (Køn)Enum.Parse(typeof(Køn), SelectKøn);
				CurrentBruger.Kønnet = criteriaKøn;
				SelectCheck = true;
			}
			else
			{
				SelectCheck = false;
			}
		}
	}
}
