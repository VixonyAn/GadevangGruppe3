using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBrugerService
    {
        Task<List<Bruger>> GetAllBrugerAsync();
        Task<Bruger> GetBrugerByIdAsync(int brugerId);
        Task<bool> CreateBrugerAsync(Bruger bruger);
        Task<bool> AdminUpdateBrugerAsync(int brugerId, Bruger bruger);
        Task<bool> BrugerUpdateBrugerAsync(int brugerId, Bruger bruger);
        Task<Bruger?> DeleteBrugerAsync(int brugerId);
        Task<Bruger?> ValidateBrugerAsync(string email, string adgangskode);
        Task<Bruger> GetBrugerByEmailAsync(string email);
        Task<List<Bruger>> FilterBrugerByBrugernavnAsync(string criteria);
        Task<bool> VerifyBrugerAsync(int brugerId, Bruger bruger);
	}
}
