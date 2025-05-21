using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface ITilmeldHoldService
    {
        /// <summary>
        /// Henter alle Tilmeldinger fra databasen og returnerer som liste
        /// </summary>
        /// <returns>Liste af Tilmeldinger</returns>
        Task<List<TilmeldHold>> GetAllTilmeldBAsync();

        /// <summary>
        /// Henter alle Tilmeldinger til en specifik Hold fra databasen og returnerer som liste
        /// </summary>
        /// <param name="holdId">HoldId på Holdet man søger efter</param>
        /// <returns>Liste af Tilmeldinger til en specifik Hold</returns>
        Task<List<TilmeldHold>> GetTilmeldHByHoldIdAsync(int holdId);

        /// <summary>
        /// Henter alle Tilmeldinger tilhørende en specifik Bruger fra databasen og returnerer som liste
        /// </summary>
        /// <param name="brugerId">BrugerId på Brugen man søger efter tilmeldinger fra</param>
        /// <returns>Liste af Tilmeldinger tilhørende en specifik Bruger</returns>
        Task<List<TilmeldHold>> GetTilmeldHByBrugerIdAsync(int brugerId);

        /// <summary>
        /// Henter en specifik Tilmelding fra databasen
        /// </summary>
        /// <param name="brugerId">BrugerId man søger efter</param>
        /// <param name="holdId">HoldId man søger efter</param>
        /// <returns>Tilmeld objekt</returns>
        Task<TilmeldHold?> GetTilmeldHByIdAsync(int brugerId, int holdId);

        /// <summary>
        /// Indsætter en ny Tilmelding (Hold) i databasen
        /// </summary>
        /// <param name="tilmeldH">TilmeldHold objekt</param>
        /// <returns>True hvis Tilmeldingen var oprettet</returns>
        Task<bool> CreateTilmeldHAsync(TilmeldHold tilmeldH);

        /// <summary>
        /// Sletter en Tilmelding (Hold) fra databasen
        /// </summary>
        /// <param name="brugerId">BrugerId på Tilmeldingen man vil slette</param>
        /// <param name="holdId">HoldId på Tilmeldingen man vil slette</param>
        /// <returns>Tilmelding (Hold) objekt</returns>
        Task<TilmeldHold?> DeleteTilmeldHAsync(int brugerId, int holdId);
    }
}
