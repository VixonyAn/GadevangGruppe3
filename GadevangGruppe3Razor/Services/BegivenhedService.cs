using Microsoft.Data.SqlClient;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3;
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
                        double? pris = reader.GetDouble("Pris");
                        string? beskrivelse = reader.GetString("Beskrivelse");
                        DateTime? dato = reader.GetDateTime("Dato");
                        int? medlemMax = reader.GetInt32("MedlemMax");
                        Begivenhed begivenhed = new Begivenhed(eventId, titel, sted, pris, beskrivelse, dato, medlemMax);
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
