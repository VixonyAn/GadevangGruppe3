using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace GadevangGruppe3Razor.Services
{
    public class BrugerService : IBrugerService
    {
        #region Instance Fields
        private String connectionString = Secret.ConnectionString;
        private string selectSql = "Select BrugerId, Brugernavn, Adgangskode, Fødselsdato, Køn, Email, Telefon, BilledUrl, Medlemskab, Position, Verificeret from Bruger";
        private string insertSql = "Insert into Bruger (BrugerID, Brugernavn, Adgangskode, Fødselsdato, Køn, Email, Telefon, BilledUrl, Medlemskab, Position, Verificeret) values (@BrugerID, @Brugernavn, @Adgangskode, @Fødselsdato, @Køn, @Email, @Telefon, @BilledUrl, @Medlemskab, @Position, @Verificeret)";
        private string deleteSql = "Delete from Bruger where BrugerId = @BrugerId";
        private string adminUpdateSql = "Update Bruger set Medlemskab = @Medlemskab, Position = @Position where BrugerId = @BrugerId";
        private string brugerUpdateSql = "Update Bruger set Brugernavn = @Brugernavn, Adgangskode = @Adgangskode, Email = @Email, Telefon = @Telefon, BilledUrl = @BilledUrl, Køn = @Køn where BrugerId = @BrugerId";
        private string loginSql = "Select Email, Adgangskode from Bruger where Email = @Email and Adgangskode = @Adgangskode";
        private string verifySql = "Update Bruger set Verificeret = @Verificeret where BrugerId = @BrugerId";
        #endregion

        #region Methods
        public async Task<bool> CreateBrugerAsync(Bruger bruger)
        {
            bool isCreated = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@BrugerId", bruger.BrugerId);
                    command.Parameters.AddWithValue("@Brugernavn", bruger.Brugernavn);
                    command.Parameters.AddWithValue("@Adgangskode", bruger.Adgangskode);
                    command.Parameters.AddWithValue("@Fødselsdato", bruger.Fødselsdato);
                    command.Parameters.AddWithValue("@Køn", bruger.Kønnet);
                    command.Parameters.AddWithValue("@Email", bruger.Email);
                    command.Parameters.AddWithValue("@Telefon", bruger.Telefon);
					command.Parameters.AddWithValue("@BilledUrl", bruger.BilledUrl);
                    command.Parameters.AddWithValue("@Medlemskab", bruger.MedlemskabsTypen);
                    command.Parameters.AddWithValue("@Position", bruger.Positionen);
					command.Parameters.AddWithValue("@Verificeret", bruger.Verificeret);
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
                    throw sqlex;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {

                }
                return bruger;
            }
        }

		public async Task<List<Bruger>> FilterBrugerByBrugernavnAsync(string criteria)
		{
			List<Bruger> brugere = new List<Bruger>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					SqlCommand command = new SqlCommand(selectSql + " where Brugernavn like @Criteria", connection);
                    command.Parameters.AddWithValue("@Criteria", "%" + criteria + "%");
					await command.Connection.OpenAsync();
					SqlDataReader reader = await command.ExecuteReaderAsync();
					while (await reader.ReadAsync())
					{
						int brugerId = reader.GetInt32("BrugerId");
						string brugernavn = reader.GetString("Brugernavn");
						string adgangskode = reader.GetString("Adgangskode");
                        DateOnly fødselsdato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Fødselsdato")));
                        Køn køn = (Køn)reader.GetInt32(reader.GetOrdinal("Køn"));
						string email = reader.GetString("Email");
						string telefon = reader.GetString("Telefon");
						string billedUrl = reader.GetString("BilledUrl");
						MedlemskabsType medlemskab = (MedlemskabsType)reader.GetInt32(reader.GetOrdinal("Medlemskab"));
						Position position = (Position)reader.GetInt32(reader.GetOrdinal("Position"));
						bool verificeret = reader.GetBoolean("Verificeret");
						Bruger bruger = new Bruger(brugerId, brugernavn, adgangskode, fødselsdato, køn, email, telefon, billedUrl, medlemskab, position, verificeret);
						if (!brugere.Contains(brugere.Find(c => c.Email.Equals(email))) || !brugere.Contains(brugere.Find(c => c.Telefon.Equals(telefon))))
						{
							brugere.Add(bruger);
						}
						else
						{
							throw new Exception("Den angivede email eller telefonnummer findes allerede i brugerlisten og kan ikke tilføjes");
						}
					}
					reader.Close();
				}
				catch (SqlException sqlExp)
				{
					Console.WriteLine("Database fejl: " + sqlExp.Message);
					throw sqlExp;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Generel fejl: " + ex.Message);
					throw ex;
				}
				finally
				{

				}
			}
			return brugere;
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
                        int brugerId = reader.GetInt32("BrugerId");
                        string brugernavn = reader.GetString("Brugernavn");
                        string adgangskode = reader.GetString("adgangskode");
                        DateOnly fødselsdato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Fødselsdato")));
                        Køn køn = (Køn)reader.GetInt32(reader.GetOrdinal("Køn"));
                        string email = reader.GetString("Email");
                        string telefon = reader.GetString("Telefon");
						string billedUrl = reader.GetString("BilledUrl");
						MedlemskabsType medlemskab = (MedlemskabsType)reader.GetInt32(reader.GetOrdinal("Medlemskab"));
                        Position position = (Position)reader.GetInt32(reader.GetOrdinal("Position"));
                        bool verificeret = reader.GetBoolean("Verificeret");
                        Bruger bruger = new Bruger(brugerId, brugernavn, adgangskode, fødselsdato, køn, email, telefon, billedUrl, medlemskab, position, verificeret);
                        if (!brugere.Contains(brugere.Find(c => c.Email.Equals(email))) || !brugere.Contains(brugere.Find(c => c.Telefon.Equals(telefon))))
                        {
							brugere.Add(bruger);
						}
                        else
                        {
                            throw new Exception("Den angivede email eller telefonnummer findes allerede i brugerlisten og kan ikke tilføjes");
                        }
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database fejl: " + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally
                {

                }
            }
            return brugere;
        }

        public async Task<Bruger> GetBrugerByEmailAsync(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Bruger bruger = new Bruger();
                try
                {
                    SqlCommand command = new SqlCommand(selectSql + " where Email = @Email", connection);
                    command.Parameters.AddWithValue("@Email", email);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) // reads from data not from console
                    {
                        int brugerId = reader.GetInt32("BrugerId");
                        string brugernavn = reader.GetString("Brugernavn");
                        string adgangskode = reader.GetString("adgangskode");
                        DateOnly fødselsdato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Fødselsdato")));
                        Køn køn = (Køn)reader.GetInt32(reader.GetOrdinal("Køn"));
                        string telefon = reader.GetString("Telefon");
                        string billedUrl = reader.GetString("BilledUrl");
                        MedlemskabsType medlemskab = (MedlemskabsType)reader.GetInt32(reader.GetOrdinal("Medlemskab"));
                        Position position = (Position)reader.GetInt32(reader.GetOrdinal("Position"));
                        bool verificeret = reader.GetBoolean("Verificeret");
                        bruger = new Bruger(brugerId, brugernavn, adgangskode, fødselsdato, køn, email, telefon, billedUrl, medlemskab, position, verificeret);
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
                finally
                {

                }
                return bruger;
            }
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
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        string brugernavn = reader.GetString("Brugernavn");
                        string adgangskode = reader.GetString("Adgangskode");
                        DateOnly fødselsdato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Fødselsdato")));
                        Køn køn = (Køn)reader.GetInt32(reader.GetOrdinal("Køn"));
                        string email = reader.GetString("Email");
                        string telefon = reader.GetString("Telefon");
                        string billedUrl = reader.GetString("BilledUrl");
						MedlemskabsType medlemskab = (MedlemskabsType)reader.GetInt32(reader.GetOrdinal("Medlemskab"));
                        Position position = (Position)reader.GetInt32(reader.GetOrdinal("Position"));
                        bool verificeret = reader.GetBoolean("Verificeret");
                        bruger = new Bruger(brugerId, brugernavn, adgangskode, fødselsdato, køn, email, telefon, billedUrl, medlemskab, position, verificeret);
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

        public async Task<bool> BrugerUpdateBrugerAsync(int brugerId, Bruger bruger)
		{
			bool isUpdated = false;
			Bruger foundBruger = await GetBrugerByIdAsync(brugerId);
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(brugerUpdateSql, connection);
					command.Parameters.AddWithValue("@BrugerId", brugerId);
					command.Parameters.AddWithValue("@Brugernavn", bruger.Brugernavn);
					command.Parameters.AddWithValue("@Adgangskode", bruger.Adgangskode);
					command.Parameters.AddWithValue("@Køn", bruger.Kønnet);
					command.Parameters.AddWithValue("@Email", bruger.Email);
					command.Parameters.AddWithValue("@Telefon", bruger.Telefon);
					command.Parameters.AddWithValue("@BilledUrl", bruger.BilledUrl);
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

		public async Task<bool> AdminUpdateBrugerAsync(int brugerId, Bruger bruger)
        {
            bool isUpdated = false;
            Bruger foundBruger = await GetBrugerByIdAsync(brugerId);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(adminUpdateSql, connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    command.Parameters.AddWithValue("@Medlemskab", bruger.MedlemskabsTypen);
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

		public async Task<Bruger?> ValidateBrugerAsync(string email, string adgangskode)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Bruger bruger = null;
				try
				{
					SqlCommand command = new SqlCommand(loginSql, connection);
					command.Parameters.AddWithValue("@Email", email);
					command.Parameters.AddWithValue("@Adgangskode", adgangskode);
					await command.Connection.OpenAsync();
					SqlDataReader reader = await command.ExecuteReaderAsync(); // breaks here - if secret string is incorrect
					if (await reader.ReadAsync())
					{
						string mail = reader.GetString("Email");
						string kode = reader.GetString("Adgangskode");
						bruger = new Bruger(mail, kode);
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
				finally 
                {

                }
				return bruger;
			}
		}

		public async Task<bool> VerifyBrugerAsync(int brugerId, Bruger bruger)
		{
            bool isVerified = false;
            Bruger foundBruger = await GetBrugerByIdAsync(brugerId);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(verifySql, connection);
                    command.Parameters.AddWithValue("@BrugerId", brugerId);
                    command.Parameters.AddWithValue("@Verificeret", bruger.Verificeret);
                    await command.Connection.OpenAsync();
                    int rowsAffected = command.ExecuteNonQuery(); 
                    if (rowsAffected > 0)
                    {
                        isVerified = true;
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
            return isVerified;
		}
		#endregion
	}
}
