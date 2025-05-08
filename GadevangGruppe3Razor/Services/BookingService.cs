using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GadevangGruppe3Razor.Services
{
    public class BookingService : IBookingService
    {
        private string _connectionString = Secret.ConnectionString;
        private string _SelectSQL = "Select BookingId,BaneId,StartTid,Bruger1,Bruger2,Beskrivelse from Booking";
        private string _InsertionString = "Insert INTO Booking Values(@BookingId,@BaneId,@StartTid,@Bruger1,@Bruger2,@Beskrivelse)";
        private string _UpdateString = "UPDATE Booking SET BookingId=@BookingId,BaneId=@BaneId,StartTid=@StartTid,Bruger1=@Bruger1,Bruger2=Bruger2,Beskrivelse=@Beskrivelse WHERE BookingId=@BookingId AND Bruger1=@Bruger1";
        private string _DeleteSql = "DELETE from Booking WHERE BookingId=@BookingId AND Bruger1=@Bruger1";


        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            List<Booking> bookinger = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_InsertionString, connection);
                    command.Parameters.AddWithValue("@BookingId", booking.BookingId);
                    command.Parameters.AddWithValue("@BaneId", booking.BaneId);
                    command.Parameters.AddWithValue("@StartTid", booking.StartTid);
                    command.Parameters.AddWithValue("@Bruger1", booking.Bruger1);
                    command.Parameters.AddWithValue("@Bruger2", booking.Bruger2);
                    command.Parameters.AddWithValue("@Beskrivelse", booking.Beskrivelse);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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
                    return false;
                }
                return true;

            }
        }

        public async Task<Booking> DeleteBookingAsync(int bookingId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            List<Booking> bookinger = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_SelectSQL, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int bookingId = reader.GetInt32("BookingId");
                        int baneId = reader.GetInt32("BaneId");
                        DateTime startTid = reader.GetDateTime("StartTid");
                        int bruger1 = reader.GetInt32("Bruger1");
                        int burger2 = reader.GetInt32("Bruger2");
                        string beskrivelse = reader.GetString("Beskrivelse");
                        Booking booking = new Booking(bookingId,baneId,startTid,bruger1,burger2,beskrivelse);
                        bookinger.Add(booking);
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

                return bookinger;
            }

        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBookingAsync(int bookingId, Booking booking)
        {
            throw new NotImplementedException();
        }

    }
}
