using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BaneFolder1
{
    public class ShowAllBaneModel : PageModel
    {
        IBaneService _baneservice;
        IBrugerService _brugerService;

        public List<Bane> Baner { get; set; }

        [BindProperty]public Bruger CurrentBruger { get; set; }

        public string Email { get; set; }

        public ShowAllBaneModel(IBaneService baneservice,IBrugerService brugerService)
        {
            _baneservice = baneservice;
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
                Baner= await _baneservice.GetAllBaneAsync();
            }
            
            catch (Exception ex)
            {
                List <Bane> baner = new List<Bane>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
            
        }
    }
}
