using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBruger
    {
        int BrugerId { get; set; }
        string Brugernavn { get; set; }
        string Adgangskode { get; set; }
        string Email { get; set; }
        string Telefon { get; set; }
        MedlemskabsType Medlemskab { get; set; }
        Position Positionen { get; set; }
        bool Verificeret { get; set; }
        string ToString();
    }
}
