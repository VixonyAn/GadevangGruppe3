using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GadevangGruppe3Razor.Services
{
    public class TilmeldBegivenhedService : Secret, ITilmeldBegivenhedService
    {
        private string connectionString = Secret.ConnectionString;
        private string selectString = "Select BrugerId, EventId, Kommentar from TilmeldBegivenhed";
        private string insertSql = "Insert into TilmeldBegivenhed (BrugerId, EventId, Kommentar) Values(@BrugerId, @EventId, @Kommentar)";
        private string updateSql = "Update TilmeldBegivenhed set Kommentar = @Kommentar where @BrugerId = BrugerId and EventId = @EventId";
        private string deleteSql = "Delete from TilmeldBegivenhed where BrugerId = @BrugerId and EventId = @EventId";

        public async Task<List<TilmeldBegivenhed>> GetAllTilmeldBAsync()
        {
            List<TilmeldBegivenhed> tilmeldBList = new List<TilmeldBegivenhed>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int brugerId = reader.GetInt32("BrugerId");
                        int eventId = reader.GetInt32("EventId");
                        string kommentar = reader.GetString("Kommentar");
                        TilmeldBegivenhed tilmeldB = new TilmeldBegivenhed(brugerId, eventId, kommentar);
                        tilmeldBList.Add(tilmeldB);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
            }
            return tilmeldBList;
        }

        public Task<List<TilmeldBegivenhed>> GetTilmeldBFromEventIdAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<TilmeldBegivenhed?> GetTilmeldBFromIdAsync(int brugerId, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateTilmeldBAsync(TilmeldBegivenhed tilmeldB)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTilmeldBAsync(int brugerId, int eventId, string kommentar)
        {
            throw new NotImplementedException();
        }

        public Task<TilmeldBegivenhed?> DeleteTilmeldBAsync(int brugerId, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
