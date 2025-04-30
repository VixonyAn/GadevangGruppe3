using GadevangGruppe3Razor.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GadevangGruppe3Razor.Models
{
    public class Begivenhed : IBegivenhed
    {
        [Required(ErrorMessage = "Titel er påkrævet")]
        [StringLength(50, ErrorMessage = "Overskredet max karakter længde af 50")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Sted er påkrævet")]
        [StringLength(60, ErrorMessage = "Overskredet max karakter længde af 60")]
        public string Sted { get; set; }

        public decimal Pris { get; set; }

        [StringLength(255, ErrorMessage = "Overskredet max karakter længde af 255")]
        public string Beskrivelse { get; set; }

        public Date Dato { get; set; }

        public int MedlemMax { get; set; }

        public override string ToString()
        {
            return $"Titel: {Titel}\n" +
                   $"Sted: {Sted} - Dato: {Dato}\n" +
                   $"Beskrivelse: {Beskrivelse}\n" +
                   $"Max antal medlemmer: {MedlemMax} - Pris pr. person {Pris}";
        }
    }
}
