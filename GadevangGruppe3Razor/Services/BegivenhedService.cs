using Microsoft.Data.SqlClient;
using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using System.Data;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GadevangGruppe3Razor.Services
{
    public class BegivenhedService : Secret, IBegivenhedService
    {
        private string connectionString = Secret.ConnectionString;
        private string selectString = "Select EventId, Titel, Sted, Dato, Beskrivelse, MedlemMax, Pris from Begivenhed";
        private string insertSql = "Insert into Begivenhed (Titel, Sted, Dato, Beskrivelse, MedlemMax, Pris) Values(@Titel, @Sted, @Dato, @Beskrivelse, @MedlemMax, @Pris)";
        private string updateSql = "Update Begivenhed set Titel = @Titel, Sted = @Sted, Dato = @Dato, Beskrivelse = @Beskrivelse, MedlemMax = @MedlemMax, Pris = @Pris where EventId = @EventId";
        private string deleteSql = "Delete from Begivenhed where EventId = @EventId";
        
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

        public async Task<Begivenhed?> GetBegivenhedFromIdAsync(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Begivenhed begivenhed = null;
                try
                {
                    SqlCommand command = new SqlCommand(selectString + " where EventId = @EventId", connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) // reads from data not from console
                    {
                        string titel = reader.GetString("Titel");
                        string sted = reader.GetString("Sted");
                        DateTime dato = reader.GetDateTime("Dato");
                        string beskrivelse = reader.GetString("Beskrivelse");
                        int medlemMax = reader.GetInt32("MedlemMax");
                        decimal? pris = reader.GetDecimal("Pris");
                        begivenhed = new Begivenhed(eventId, titel, sted, dato, beskrivelse, medlemMax, pris);
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
                return begivenhed;
            }
        }

        public async Task<bool> CreateBegivenhedAsync(Begivenhed begivenhed)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    //command.Parameters.AddWithValue("@EventId", begivenhed.EventId);
                    command.Parameters.AddWithValue("@Titel", begivenhed.Titel);
                    command.Parameters.AddWithValue("@Sted", begivenhed.Sted);
                    command.Parameters.AddWithValue("@Dato", begivenhed.Dato);
                    command.Parameters.AddWithValue("@Beskrivelse", begivenhed.Beskrivelse);
                    command.Parameters.AddWithValue("@MedlemMax", begivenhed.MedlemMax);
                    command.Parameters.AddWithValue("@Pris", begivenhed.Pris);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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
                return true;
            }
        }

        public async Task<bool> UpdateBegivenhedAsync(Begivenhed begivenhed, int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<Begivenhed?> DeleteBegivenhedAsync(int eventId)
        {
            Begivenhed? begivenhed = await GetBegivenhedFromIdAsync(eventId);
            if (begivenhed == null) { return null; }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 0) { return null; }
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
                return begivenhed;
            }
        }
    }
}
