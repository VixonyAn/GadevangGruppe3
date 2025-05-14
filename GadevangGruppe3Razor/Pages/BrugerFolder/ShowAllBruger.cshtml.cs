using GadevangGruppe3Razor.Helper;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class ShowAllBrugerModel : PageModel
    {
        private IBrugerService _brugerService;
        
        public List<Bruger> Brugere { get; set; }
        [BindProperty(SupportsGet = true)] public string FilterCriteriaBrugernavn { get; set; }
        [BindProperty(SupportsGet = true)] public string FilterCriteriaMedlemskab { get; set; }
        [BindProperty(SupportsGet = true)] public string FilterCriteriaPosition { get; set; }
        [BindProperty(SupportsGet = true)] public string SortOrder { get; set; }
        [BindProperty(SupportsGet = true)] public string SortOrderAscDesc { get; set; }
        [BindProperty] public Bruger CurrentBruger { get; set; }
        public string Email { get; set; }

        public ShowAllBrugerModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }
        public async Task<IActionResult> OnGetAsync()
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
                    CurrentBruger = await _brugerService.GetBrugerByEmailAsync(Email);
                }
				if (!string.IsNullOrEmpty(FilterCriteriaBrugernavn))
				{
					Brugere = await _brugerService.FilterBrugerByBrugernavnAsync(FilterCriteriaBrugernavn);
				}
				else
				{
					Brugere = await _brugerService.GetAllBrugerAsync();
				}
				if (SortOrder == "Brugernavn")
                {
                    Brugere.Sort(new BrugernavnComparer());
                }
                if (SortOrderAscDesc == "Descending")
                {
                    Brugere.Reverse();
                }
                
				FilterBrugerMedlemskab();
				FilterBrugerPosition();
			}
            catch (Exception ex)
            {
                Brugere = new List<Bruger>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
		}

        public async Task OnGetFilter()
        {
            await OnGetAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int brugerId)
        {
            return RedirectToPage("Delete", new { brugerId = brugerId });
        }

        public async Task<IActionResult> OnPostUpdateAsync(int brugerId)
        {
            return RedirectToPage("Update", new { brugerId = brugerId });
        }

        private void FilterBrugerMedlemskab()
        {
            if (!FilterCriteriaMedlemskab.IsNullOrEmpty() && FilterCriteriaMedlemskab != "All")
            {
                MedlemskabsType criteriaMedlemskab = (MedlemskabsType)Enum.Parse(typeof(MedlemskabsType), FilterCriteriaMedlemskab);
				Brugere = Brugere.FindAll(b => b.Medlemskab == criteriaMedlemskab);
            }
        }

        private void FilterBrugerPosition()
        {
            if (!FilterCriteriaPosition.IsNullOrEmpty() && FilterCriteriaPosition != "All")
            {
                Position criteriaPosition = (Position)Enum.Parse(typeof(Position), FilterCriteriaPosition);
                Brugere = Brugere.FindAll(b => b.Positionen == criteriaPosition);
            }
        }
    }
}
