using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages
{
    public class IndexModel : PageModel
    {
        #region Instance Fields
        private readonly ILogger<IndexModel> _logger;
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public Bruger CurrentBruger { get; set; }
        public string Email { get; set; }
        #endregion

        #region Constructor
        public IndexModel(ILogger<IndexModel> logger, IBrugerService brugerService)
        {
            _logger = logger;
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync() // OnGet k�rer n�r siden indl�ses
        { // await l�ser den del af applikationen som afh�nger af dataen der ventes p�, mens resten af programmet kan blive ved med at k�re
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
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
        #endregion
    }
}
