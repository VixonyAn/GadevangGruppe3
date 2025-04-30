using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GadevangGruppe3Razor.Interfaces
{
    public interface IBegivenhed
    {
        string Titel { get; set; }
        string Sted { get; set; }
        decimal Pris { get; set; }
        string Beskrivelse { get; set; }
        Date Dato { get; set; }
        int MedlemMax { get; set; }
        string ToString();
    }
}
