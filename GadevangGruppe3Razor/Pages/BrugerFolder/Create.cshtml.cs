using GadevangGruppe3Razor.Helper;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class CreateModel : PageModel
    {
		private IBrugerService _brugerService;
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty] public Bruger Bruger { get; set; }
        [BindProperty] public int BrugerId { get; set; }
        [BindProperty] public IFormFile? Billed { get; set; }
        [BindProperty] public string SelectKøn { get; set; }
        public string MessageError { get; set; }
        public string MessageErrorInvalidAlder { get; set; }
        public bool SelectCheck { get; set; }

        public CreateModel(IBrugerService brugerService, IWebHostEnvironment webHostEnvironment)
        {
            _brugerService = brugerService;
            _webHostEnvironment = webHostEnvironment;
        }
        public void OnGet(int brugerId)
        {
            Bruger = new Bruger();
            BrugerId = brugerId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Billed == null)
            {
                Bruger.BilledUrl = "defaultImage.jpg";
            }
            if (Billed != null)
            {
                if (Bruger.BilledUrl != null)
                {
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images", Bruger.BilledUrl);
					System.IO.File.Delete(filePath);
				}
				Bruger.BilledUrl = ProcessUploadedFile();
			}
            try
            {
                SelectBrugerKøn();
				if (!SelectCheck)
				{
					MessageError = "Du mangler at vælge en mulighed";
					return Page();
				}
				BestemMedlemskabsType bestemMedlemskab = new BestemMedlemskabsType();
                int alder = bestemMedlemskab.CalculateAlder(Bruger.Fødselsdato);
                if (alder >= 5)
                {
					Bruger.MedlemskabsTypen = bestemMedlemskab.BestemMedlemskabsTypeFraAlder(alder);
				}
				else
                {
                    MessageErrorInvalidAlder = "Du er for ung til at være et medlem";
                    return Page();
                }
                Bruger.Positionen = Position.Medlem;
				await _brugerService.CreateBrugerAsync(Bruger);
				return RedirectToPage("ShowAllBruger", new { BrugerId = BrugerId });
			}
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("ShowAllBruger", new { BrugerId = BrugerId });
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Billed != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Billed.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Billed.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

		private void SelectBrugerKøn()
		{
			if (!SelectKøn.IsNullOrEmpty() && SelectKøn != "Select")
			{
				Køn criteriaKøn = (Køn)Enum.Parse(typeof(Køn), SelectKøn);
				Bruger.Kønnet = criteriaKøn;
				SelectCheck = true;
			}
			else
			{
				SelectCheck = false;
			}
		}
    }
}
