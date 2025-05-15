using GadevangGruppe3Razor.Interfaces;

namespace GadevangGruppe3Razor.Models
{
    public class Booking : IBooking
    {
        public int BookingId { get; set; }

        public int BaneId { get; set; }
        public DateOnly Dato { get; set; }

        public int StartTid { get; set; }
        public int Bruger1 { get; set; }
        public int Bruger2 { get; set; }
        public string Beskrivelse { get; set; }

        public Booking()
        {
            
        }
        public Booking(int bookingId, int baneId, DateOnly dato, int startTid, int bruger1, string beskrivelse)
        {
            BookingId = bookingId;
            BaneId = baneId;
            Dato = dato;
            StartTid = startTid;
            Bruger1 = bruger1;
            Beskrivelse = beskrivelse;
        }
        public Booking(int bookingId,int baneId, DateOnly dato, int startTid, int bruger1, int bruger2, string beskrivelse)
        {
            BookingId = bookingId;
            BaneId = baneId;
            Dato = dato;
            StartTid = startTid;
            Bruger1 = bruger1;
            Bruger2 = bruger2;
            Beskrivelse = beskrivelse;
        }

        public override string ToString()
        {
            return $"Booking id: {BookingId}, BaneId: {BaneId} DatoTid: {Dato},{StartTid}\n" +
                   $"Bruger 1: {Bruger1}, Bruger 2: {Bruger2} Beskrivelse: {Beskrivelse}";
        }
    }
}
