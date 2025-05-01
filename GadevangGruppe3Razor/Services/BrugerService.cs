using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace GadevangGruppe3Razor.Services
{
    public class BrugerService : IBrugerService
    {
        private String connectionString = Secret.ConnectionString;
        private string selectSql = "Select BrugerId, Brugernavn, Adgangskode, Email, Telefon, Verificeret, Medlemskab, Position from Bruger";
        private string insertSql = "Insert into Bruger (Brugernavn, Adgangskode, Email, Telefon, Verificeret, Medlemskab, Position) values (@Brugernavn, @Adgangskode, @Email, @Telefon, @Verificeret, @Medlemskab, @Position)";
        private string deleteSql = "Delete from Bruger where BrugerId = @BrugerId";
        private string updateSql = "Update Bruger set Username = @Username, Adgangskode = @Adgangskode, Email = @Email, Telefon = @Telefon, Verificeret = @Verificeret, Medlemskab = @Medlemskab, Position = @Position where BrugerId = @BrugerId";
        public async Task<bool> CreateBrugerAsync(Bruger bruger)
        {
            bool isCreated = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@Brugernavn", bruger.Brugernavn);
                    command.Parameters.AddWithValue("@Adgangskode", bruger.Adgangskode);
                    command.Parameters.AddWithValue("@Email", bruger.Email);
                    command.Parameters.AddWithValue("@Telefon", bruger.Telefon);
                    command.Parameters.AddWithValue("@Verificeret", bruger.Verificeret);
                    command.Parameters.AddWithValue("@Medlemskab", bruger.Medlemskab);
                    command.Parameters.AddWithValue("@Position", bruger.Positionen);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        isCreated = true;
                    }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine(sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {

                }
            }
            return isCreated;
        }

        public async Task<Bruger?> DeleteBrugerAsync(int brugerId)
        {
            Bruger? bruger = await GetBrugerByIdAsync(brugerId);
            if (bruger == null)
            {
                return null;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    await connection.OpenAsync();
                    int noOfRows = command.ExecuteNonQuery();
                    if (noOfRows == 0)
                    {
                        return null;
                    }
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine(sqlex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {

                }
                return bruger;
            }
        }

        public async Task<List<Bruger>> GetAllBrugerAsync()
        {
            List<Bruger> brugere = new List<Bruger>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectSql, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        string brugernavn = reader.GetString("Brugernavn");
                        string adgangskode = reader.GetString("adgangskode");
                        string email = reader.GetString("Email");
                        string telefon = reader.GetString("Telefon");
                        MedlemskabsType medlemskab = (MedlemskabsType)reader.GetInt32(reader.GetOrdinal("Medlemskab"));
                        Position position = (Position)reader.GetInt32(reader.GetOrdinal("Position"));
                        bool verificeret = reader.GetBoolean("Verificeret");
                        Bruger bruger = new Bruger(brugernavn, adgangskode, email, telefon, medlemskab, position, verificeret);
                        brugere.Add(bruger);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return brugere;
        }

        public async Task<Bruger> GetBrugerByIdAsync(int brugerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Bruger bruger = null;
                try
                {
                    SqlCommand command = new SqlCommand(selectSql + " WHERE BrugerId = @BrugerId", connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", brugerId);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        string brugernavn = reader.GetString("Brugernavn");
                        string adgangskode = reader.GetString("Adgangskode");
                        string email = reader.GetString("Email");
                        string telefon = reader.GetString("Telefon");
                        MedlemskabsType medlemskab = (MedlemskabsType)reader.GetInt32(reader.GetOrdinal("Medlemskab"));
                        Position position = (Position)reader.GetInt32(reader.GetOrdinal("Position"));
                        bool verificeret = reader.GetBoolean("Verificeret");
                        bruger = new Bruger(brugernavn, adgangskode, email, telefon, medlemskab, position, verificeret);
                    }
                    reader.Close();
                    return bruger;
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine(sqlex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {

                }
                return bruger;
            }
        }

        public async Task<bool> UpdateBrugerAsync(int brugerId, Bruger bruger)
        {
            bool isUpdated = false;
            Bruger foundBruger = await GetBrugerByIdAsync(brugerId);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@Brugernavn", bruger.Brugernavn);
                    command.Parameters.AddWithValue("@Adgangskode", bruger.Adgangskode);
                    command.Parameters.AddWithValue("@Email", bruger.Email);
                    command.Parameters.AddWithValue("@Telefon", bruger.Telefon);
                    command.Parameters.AddWithValue("@Verificeret", bruger.Verificeret);
                    command.Parameters.AddWithValue("@Medlemskab", bruger.Medlemskab);
                    command.Parameters.AddWithValue("@Position", bruger.Positionen);
                    await command.Connection.OpenAsync();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        isUpdated = true;
                    }
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine(sqlex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            return isUpdated;
        }
    }
}
