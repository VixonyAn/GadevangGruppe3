using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBegivenhedService
    {
        /// <summary>
        /// Henter alle begivenheder fra databasen og returnerer som liste
        /// </summary>
        /// <returns>Liste af Begivenheder</returns>
        Task<List<Begivenhed>> GetAllBegivenhedAsync();

        /// <summary>
        /// Henter en specifik begivenhed fra databasen
        /// </summary>
        /// <param name="eventId">EventId man søger efter</param>
        /// <returns>Begivenhed objekt</returns>
        Task<Begivenhed?> GetBegivenhedFromIdAsync(int eventId);

        /// <summary>
        /// Indsætter et nyt begivenhed i databasen
        /// </summary>
        /// <param name="begivenhed">Begivenhed objekt</param>
        /// <returns>True hvis Begivenheden var oprettet</returns>
        Task<bool> CreateBegivenhedAsync(Begivenhed begivenhed);

        /// <summary>
        /// Opdaterer en begivenhed i databasen
        /// </summary>
        /// <param name="begivenhed">Ny Begivenhed objekt</param>
        /// <param name="eventId">EventId af Begivenheden der bliver ændret</param>
        /// <returns>True hvis Begivenheden var opdateret</returns>
        Task<bool> UpdateBegivenhedAsync(Begivenhed begivenhed, int eventId);

        /// <summary>
        /// Sletter en begivenhed fra databasen
        /// </summary>
        /// <param name="eventId">EventId af Begivenheden man vil slette</param>
        /// <returns>Begivenhed objekt</returns>
        Task<Begivenhed?> DeleteBegivenhedAsync(int eventId);
    }
}
