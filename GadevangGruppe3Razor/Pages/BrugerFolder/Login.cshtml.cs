using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Services;
using GadevangGruppe3Razor.Models;
namespace GadevangGruppe3Razor.Pages.BrugerFolder
{
    public class LoginModel : PageModel
    {
        #region Instance Fields
        private IBrugerService _brugerService;
        #endregion

        #region Properties
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Adgangskode { get; set; }
        public string MessageError { get; set; }
        #endregion

        #region Constructors
        public LoginModel(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync() 
        {

        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            return RedirectToPage("Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) 
            { 
                return Page(); 
            }
            try
            {
                Bruger? loginBruger = await _brugerService.ValidateBrugerAsync(Email, Adgangskode);
                if (loginBruger != null)
                {
                    HttpContext.Session.SetString("Email", loginBruger.Email);
                    return RedirectToPage("/Index");
                }
                else
                { // if user cannot be verified
                    MessageError = "Invalid email eller adgangskode";
                    return Page();
                }
            }
            catch (Exception ex)
            { // catches exceptions like weak connection or mistake in query
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }
        #endregion
    }
}
