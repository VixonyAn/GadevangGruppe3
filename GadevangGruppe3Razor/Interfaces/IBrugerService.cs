using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBrugerService
    {
        Task<List<Bruger>> GetAllBrugerAsync();
        Task<Bruger> GetBrugerByIdAsync(int brugerId);
        Task<bool> CreateBrugerAsync(Bruger bruger);
        Task<bool> UpdateBrugerAsync(int brugerId, Bruger bruger);
        Task<Bruger?> DeleteBrugerAsync(int brugerId);
        Task<Bruger?> ValidateBrugerAsync(string email, string adgangskode);
	}
}
