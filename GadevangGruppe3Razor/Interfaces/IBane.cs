using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBane
    {
        int BaneId { get; }
        BaneType Type { get; set; }

        BaneMiljø Miljø { get; set; }

        string Beskrivelse { get; set; }

        string ToString();

    }
}
