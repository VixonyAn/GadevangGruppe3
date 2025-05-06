using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadevangGruppe3Razor.Pages.BaneFolder1
{
    public class ShowAllBane1Model : PageModel
    {
        IBaneService _baneservice;

        public List<Bane> Baner { get; set; }

        public ShowAllBane1Model(IBaneService baneservice)
        {
            _baneservice = baneservice;
        }

        public async Task OnGetAsync()
        {
            try 
            { 
                Baner=await _baneservice.GetAllBaneAsync();
            } 
            catch (Exception ex)
            {
                List <Bane> baner = new List<Bane>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            
        }
    }
}
