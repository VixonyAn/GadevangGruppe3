using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface ITilmeldBegivenhedService
    {
        /// <summary>
        /// Henter alle Tilmeldinger fra databasen og returnerer som liste
        /// </summary>
        /// <returns>Liste af Tilmeldinger</returns>
        Task<List<TilmeldBegivenhed>> GetAllTilmeldBAsync();

        /// <summary>
        /// Henter alle Tilmeldinger til en specifik Begivenhed fra databasen og returnerer som liste
        /// </summary>
        /// <param name="eventId">EventId på Begivenheden man søger efter</param>
        /// <returns>Liste af Tilmeldinger til en specifik Begivenhed</returns>
        Task<List<TilmeldBegivenhed>> GetTilmeldBByEventIdAsync(int eventId);

        /// <summary>
        /// Henter alle Tilmeldinger tilhørende en specifik Bruger fra databasen og returnerer som liste
        /// </summary>
        /// <param name="brugerId">BrugerId på Brugen man søger efter tilmeldinger fra</param>
        /// <returns>Liste af Tilmeldinger tilhørende en specifik Bruger</returns>
        Task<List<TilmeldBegivenhed>> GetTilmeldBByBrugerIdAsync(int brugerId);

        /// <summary>
        /// Henter en specifik Tilmelding fra databasen
        /// </summary>
        /// <param name="brugerId">BrugerId man søger efter</param>
        /// <param name="eventId">EventId man søger efter</param>
        /// <returns>Tilmeld objekt</returns>
        Task<TilmeldBegivenhed?> GetTilmeldBByIdAsync(int brugerId, int eventId);

        /// <summary>
        /// Indsætter en ny Tilmelding (Begivenhed) i databasen
        /// </summary>
        /// <param name="tilmeldB">TilmeldBegivenhed objekt</param>
        /// <returns>True hvis Tilmeldingen var oprettet</returns>
        Task<bool> CreateTilmeldBAsync(TilmeldBegivenhed tilmeldB);

        /// <summary>
        /// Opdaterer en Tilmelding (Begivenhed) i databasen
        /// </summary>
        /// <param name="brugerId">BrugerId af den tilmeldte Bruger</param>
        /// <param name="eventId">EventId af den tilmelde Begivenhed der bliver ændret</param>
        /// <param name="kommentar">Kommentaren der bliver ændret på tilmeldingen</param>
        /// <returns>True hvis Tilmelding (Begivenhed) var opdateret</returns>
        Task<bool> UpdateTilmeldBAsync(int brugerId, int eventId, string kommentar);

        /// <summary>
        /// Sletter en Tilmelding (Begivenhed) fra databasen
        /// </summary>
        /// <param name="brugerId">BrugerId på Tilmeldingen man vil slette</param>
        /// <param name="eventId">EventId på Tilmeldingen man vil slette</param>
        /// <returns>Tilmelding (Begivenhed) objekt</returns>
        Task<TilmeldBegivenhed?> DeleteTilmeldBAsync(int brugerId, int eventId);
    }
}
