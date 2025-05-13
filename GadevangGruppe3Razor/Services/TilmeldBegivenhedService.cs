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

        public async Task<List<TilmeldBegivenhed>> GetTilmeldBByEventIdAsync(int eventId)
        {
            List<TilmeldBegivenhed> tilmeldBList = new List<TilmeldBegivenhed>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectString + " where EventId = @EventId", connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int brugerId = reader.GetInt32("BrugerId");
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

        public async Task<List<TilmeldBegivenhed>> GetTilmeldBByBrugerIdAsync(int brugerId)
        {
            List<TilmeldBegivenhed> tilmeldBList = new List<TilmeldBegivenhed>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectString + " where BrugerId = @BrugerId", connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
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

        public async Task<TilmeldBegivenhed?> GetTilmeldBByIdAsync(int brugerId, int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                TilmeldBegivenhed tilmeldB = null;
                try
                {
                    SqlCommand command = new SqlCommand(selectString + " where BrugerId = @BrugerId and EventId = @EventId", connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) // reads from data not from console
                    {
                        int bId = reader.GetInt32("BrugerId");
                        int eId = reader.GetInt32("EventId");
                        string kom = reader.GetString("Kommentar");
                        tilmeldB = new TilmeldBegivenhed(bId, eId, kom);
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
                return tilmeldB;
            }
        }

        public async Task<bool> CreateTilmeldBAsync(TilmeldBegivenhed tilmeldB)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@BrugerId", tilmeldB.BrugerId);
                    command.Parameters.AddWithValue("@EventId", tilmeldB.EventId);
                    command.Parameters.AddWithValue("@Kommentar", tilmeldB.Kommentar);
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

        public async Task<bool> UpdateTilmeldBAsync(int brugerId, int eventId, string kommentar)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    command.Parameters.AddWithValue("@Kommentar", kommentar);
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

        public async Task<TilmeldBegivenhed?> DeleteTilmeldBAsync(int brugerId, int eventId)
        {
            TilmeldBegivenhed? tilmeldB = await GetTilmeldBByIdAsync(brugerId, eventId);
            if (tilmeldB == null) { return null; }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
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
                return tilmeldB;
            }
        }
    }
}
