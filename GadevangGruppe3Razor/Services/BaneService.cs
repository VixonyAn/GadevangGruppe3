using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GadevangGruppe3Razor.Services
{
    public class BaneService : IBaneService
    {
        private string _connectionString = Secret.ConnectionString;
        private string _SelectSQL = "Select BaneId,BaneType,BaneMiljø,Beskrivelse from Bane";
        private string _InsertionString = "Insert INTO Bane (@BaneId,BaneType,BaneMiljø,Beskrivelse)  Values(@BaneType,@BaneMiljø,@Beskrivelse)";


        public async void CreateBaneAsync(Bane bane)
        {
            List<Bane> baner = new List<Bane>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_SelectSQL, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@BaneId", bane.BaneId);
                    command.Parameters.AddWithValue("@BaneType",bane.Type);
                    command.Parameters.AddWithValue("@BaneMiljø", bane.Miljø);
                    command.Parameters.AddWithValue("@Beskrivelse", bane.Beskrivelse);
                    //await command.ExecuteNonQueryAsync();
                    await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                    throw ex;
                }

            }
        }





        public void DeleteBaneAsync(int baneId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Bane>> GetAllBaneAsync()
        {
            List<Bane> baner = new List<Bane>();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try 
                {
                     SqlCommand command = new SqlCommand(_SelectSQL,connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int baneId = reader.GetInt32("BaneId");
                        BaneType baneType = (BaneType)reader.GetInt32(reader.GetOrdinal("BaneType"));
                        BaneMiljø miljø = (BaneMiljø)reader.GetInt32(reader.GetOrdinal("BaneMiljø"));
                        string beskrivelse = reader.GetString("Beskrivelse");
                        Bane bane = new Bane(baneId, baneType, miljø, beskrivelse);
                        baner.Add(bane);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("General error: " + ex.Message);
                    throw ex;
                }

                return baner;
            }

        }

        public Bane GetBaneByIdAsync(int baneId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBaneAsync(int baneId, Bane bane)
        {
            throw new NotImplementedException();
        }
    }
}
