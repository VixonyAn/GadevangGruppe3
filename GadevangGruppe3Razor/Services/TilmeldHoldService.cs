using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GadevangGruppe3Razor.Services
{
    public class TilmeldHoldService : Secret, ITilmeldHoldService
    {
        private string connectionString = Secret.ConnectionString;
        private string selectString = "Select BrugerId, HoldId from TilmeldHold";
        private string insertSql = "Insert into TilmeldHold (BrugerId, HoldId) Values(@BrugerId, @HoldId)";
        private string deleteSql = "Delete from TilmeldHold where BrugerId = @BrugerId and HoldId = @HoldId";


        public async Task<List<TilmeldHold>> GetAllTilmeldBAsync()
        {
            List<TilmeldHold> tilmeldHList = new List<TilmeldHold>();
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
                        int holdId = reader.GetInt32("HoldId");
                        TilmeldHold tilmeldH = new TilmeldHold(brugerId, holdId);
                        tilmeldHList.Add(tilmeldH);
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
            return tilmeldHList;
        }

        public async Task<List<TilmeldHold>> GetTilmeldHByHoldIdAsync(int holdId)
        {
            List<TilmeldHold> tilmeldHList = new List<TilmeldHold>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectString + " where HoldId = @HoldId", connection);
                    command.Parameters.AddWithValue("@HoldId", holdId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int brugerId = reader.GetInt32("BrugerId");
                        TilmeldHold tilmeldH = new TilmeldHold(brugerId, holdId);
                        tilmeldHList.Add(tilmeldH);
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
            return tilmeldHList;
        }

        public async Task<List<TilmeldHold>> GetTilmeldHByBrugerIdAsync(int brugerId)
        {
            List<TilmeldHold> tilmeldHList = new List<TilmeldHold>();
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
                        int holdId = reader.GetInt32("HoldId");
                        TilmeldHold tilmeldH = new TilmeldHold(brugerId, holdId);
                        tilmeldHList.Add(tilmeldH);
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
            return tilmeldHList;
        }
        

        public async Task<TilmeldHold?> GetTilmeldHByIdAsync(int brugerId, int holdId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                TilmeldHold tilmeldH = null;
                try
                {
                    SqlCommand command = new SqlCommand(selectString + " where BrugerId = @BrugerId and HoldId = @HoldId", connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    command.Parameters.AddWithValue("@HoldId", holdId);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) // reads from data not from console
                    {
                        int bId = reader.GetInt32("BrugerId");
                        int hId = reader.GetInt32("HoldId");
                        tilmeldH = new TilmeldHold(bId, hId);
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
                return tilmeldH;
            }
        }

        public async Task<bool> CreateTilmeldHAsync(TilmeldHold tilmeldH)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@BrugerId", tilmeldH.BrugerId);
                    command.Parameters.AddWithValue("@HoldId", tilmeldH.HoldId);
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

        public async Task<TilmeldHold?> DeleteTilmeldHAsync(int brugerId, int holdId)
        {
            TilmeldHold? tilmeldH = await GetTilmeldHByIdAsync(brugerId, holdId);
            if (tilmeldH == null) { return null; }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    command.Parameters.AddWithValue("@HoldId", holdId);
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
                return tilmeldH;
            }
        }
    }
}
