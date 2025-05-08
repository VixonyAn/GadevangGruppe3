using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBooking
    {
        int BookingId { get; set; }

        int BaneId { get; set; }

        DateOnly Dato { get; set; }
        int Tid { get; set; }
        int Bruger1 { get; set; }

        int Bruger2 { get; set; }

        string Beskrivelse { get; set; }
    }
}
