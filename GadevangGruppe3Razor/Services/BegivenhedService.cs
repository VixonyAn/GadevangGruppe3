using Microsoft.Data.SqlClient;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace GadevangGruppe3Razor.Services
{
    public class BegivenhedService : Secret, IBegivenhedService
    {
        private string connectionString = Secret.ConnectionString;
        private string selectString = "Select EventId, Titel, Sted, Pris, Beskrivelse, Dato, MedlemMax from EventTable";
        private string insertSql = "Insert into EventTable Values(@ID, @Titel, @Sted, @Pris, @Beskrivelse, @Dato, @MedlemMax)";
        private string updateSql = "Update EventTable set Titel = @Titel, Sted = @Sted, Pris = @Pris, Beskrivelse = @Beskrivelse, Dato = @Dato, MedlemMax = @MedlemMax where EventId = @ID";
        private string deleteSql = "Delete from EventTable where EventId = @ID";

        public async Task<List<Begivenhed>> GetAllBegivenhedAsync()
        {
            List<Begivenhed> begivenheder = new List<Begivenhed>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int eventId = reader.GetInt32("EventId");
                        string titel = reader.GetString("Titel");
                        string sted = reader.GetString("Sted");
                        DateTime dato = reader.GetDateTime("Dato");
                        string beskrivelse = reader.GetString("Beskrivelse");
                        int medlemMax = reader.GetInt32("MedlemMax");
                        decimal? pris = reader.GetDecimal("Pris");
                        Begivenhed begivenhed = new Begivenhed(eventId, titel, sted, dato, beskrivelse, medlemMax, pris);
                        begivenheder.Add(begivenhed);
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
            return begivenheder;
        }

        public Task<Begivenhed?> GetBegivenhedFromIdAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateBegivenhedAsync(Begivenhed begivenhed)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBegivenhedAsync(Begivenhed begivenhed, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<Begivenhed?> DeleteBegivenhedAsync(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
