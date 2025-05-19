using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;

namespace GadevangGruppe3Razor.Services
{
	public class HoldService : IHoldService
	{
		private String connectionString = Secret.ConnectionString;
		private string selectSql = "Select HoldId, Holdnavn, Instruktørnavn, StartDato, SlutDato, Tid, Sted, Pris, MaxMedlemstal from Hold";
		private string insertSql = "Insert into Hold (HoldId, Holdnavn, Instruktørnavn, StartDato, SlutDato, Tid, Sted, Pris, MaxMedlemskab) values (@HoldId, @Holdnavn, @Instruktørnavn, @StartDato, @SlutDato, @Tid, @Sted, @Pris, @MaxMedlemskab)";
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
					command.Parameters.AddWithValue("@Instruktørnavn", hold.Holdnavn);
					command.Parameters.AddWithValue("@StartDato", hold.StartDato);
					command.Parameters.AddWithValue("@SlutDato", hold.SlutDato);
					command.Parameters.AddWithValue("@Tid", hold.Tid);
					command.Parameters.AddWithValue("@Sted", hold.Sted);
					command.Parameters.AddWithValue("@Pris", hold.Pris);
					command.Parameters.AddWithValue("@MaxMedlemstal", hold.MaxMedlemstal);
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

		public Task<Hold?> DeleteHoldAsync(int holdId)
		{
			throw new NotImplementedException();
		}

		public Task<List<Hold>> GetAllHoldAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Hold> GetHoldByIdAsync(int holdId)
		{
			throw new NotImplementedException();
		}
	}
}
