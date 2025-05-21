using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GadevangGruppe3Razor.Services
{
	public class HoldService : IHoldService
	{
		private String connectionString = Secret.ConnectionString;
		private string selectSql = "Select HoldId, Holdnavn, Instruktørnavn, StartDato, SlutDato, Tid, Sted, Pris, MaxMedlemstal from Hold";
		private string insertSql = "Insert into Hold (HoldId, Holdnavn, Instruktørnavn, StartDato, SlutDato, Tid, Sted, Pris, MaxMedlemstal) values (@HoldId, @Holdnavn, @Instruktørnavn, @StartDato, @SlutDato, @Tid, @Sted, @Pris, @MaxMedlemstal)";
		private string deleteSql = "Delete from Hold where HoldId = @HoldId";

		public async Task<bool> CreateHoldAsync(Hold hold)
		{
			bool isCreated = false;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					SqlCommand command = new SqlCommand(insertSql, connection);
					await command.Connection.OpenAsync();
					command.Parameters.AddWithValue("@HoldId", hold.HoldId);
					command.Parameters.AddWithValue("@Holdnavn", hold.Holdnavn);
					command.Parameters.AddWithValue("@Instruktørnavn", hold.Instruktørnavn);
					command.Parameters.AddWithValue("@StartDato", hold.StartDato);
					command.Parameters.AddWithValue("@SlutDato", hold.SlutDato);
					command.Parameters.AddWithValue("@Tid", hold.Tid);
					command.Parameters.AddWithValue("@Sted", hold.Sted);
					command.Parameters.AddWithValue("@Pris", hold.Pris);
					if (hold.MaxMedlemstal >= 5 && hold.MaxMedlemstal <= 9)
					{
						command.Parameters.AddWithValue("@MaxMedlemstal", hold.MaxMedlemstal);
					}
					else
					{
						throw new Exception("Det maximale medlemstal er udenfor det lovlige interval");
					}
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

		public async Task<Hold?> DeleteHoldAsync(int holdId)
		{
			Hold? hold = await GetHoldByIdAsync(holdId);
			if (hold == null)
			{
				return null;
			}
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					SqlCommand command = new SqlCommand(deleteSql, connection);
					command.Parameters.AddWithValue("@HoldId", holdId);
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
				return hold;
			}
		}

		public async Task<List<Hold>> GetAllHoldAsync()
		{
			List<Hold> holdListe = new List<Hold>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					SqlCommand command = new SqlCommand(selectSql, connection);
					await command.Connection.OpenAsync();
					SqlDataReader reader = await command.ExecuteReaderAsync();
					while (await reader.ReadAsync())
					{
						int holdId = reader.GetInt32("HoldId");
						string holdnavn = reader.GetString("Holdnavn");
						string instruktørnavn = reader.GetString("Instruktørnavn");
						DateOnly startDato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDato")));
						DateOnly slutDato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("SlutDato")));
						string tid = reader.GetString("Tid");
						string sted = reader.GetString("Sted");
						double pris = reader.GetDouble("Pris");
						int maxMedlemstal = reader.GetInt32("MaxMedlemstal");
						Hold hold = new Hold(holdId, holdnavn, instruktørnavn, startDato, slutDato, tid, sted, pris, maxMedlemstal);
						if (!holdListe.Contains(holdListe.Find(h => h.Holdnavn == holdnavn)))
						{
							holdListe.Add(hold);
						}
						else
						{
							throw new Exception("Det angivede holdnavn findes allerede i listen og kan ikke tilføjes");
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
			return holdListe;
		}

		public async Task<Hold> GetHoldByIdAsync(int holdId)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Hold hold = null;
				try
				{
					SqlCommand command = new SqlCommand(selectSql + " WHERE HoldId = @HoldId", connection);
					await command.Connection.OpenAsync();
					command.Parameters.AddWithValue("@HoldId", holdId);
					SqlDataReader reader = await command.ExecuteReaderAsync();
					if (await reader.ReadAsync())
					{
						string holdnavn = reader.GetString("Holdnavn");
						string instruktørnavn = reader.GetString("Instruktørnavn");
						DateOnly startDato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDato")));
						DateOnly slutDato = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("SlutDato")));
						string tid = reader.GetString("Tid");
						string sted = reader.GetString("Sted");
						double pris = reader.GetDouble("Pris");
						int maxMedlemstal = reader.GetInt32("MaxMedlemstal");
						hold = new Hold(holdId, holdnavn, instruktørnavn, startDato, slutDato, tid, sted, pris, maxMedlemstal);
					}
					reader.Close();
					return hold;
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
				return hold;
			}
		}
	}
}
