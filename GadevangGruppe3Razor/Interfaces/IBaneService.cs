using GadevangGruppe3Razor.Models;
using System.Data;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBaneService
    {
        Task<List<Bane>> GetAllBaneAsync();
        Task<Bane> GetBaneByIdAsync(int baneId);
        void CreateBaneAsync(Bane bane);

        void UpdateBaneAsync(int baneId, Bane bane);

        void DeleteBaneAsync(int baneId);

    }
}
