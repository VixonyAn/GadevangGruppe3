using GadevangGruppe3Razor.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GadevangGruppe3Razor.Models
{
    public class Begivenhed : IBegivenhed
    {
        #region Properties
        [Required(ErrorMessage = "EventId er påkrævet")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Titel er påkrævet")]
        [StringLength(50, ErrorMessage = "Overskredet max karakter længde af 50")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Sted er påkrævet")]
        [StringLength(60, ErrorMessage = "Overskredet max karakter længde af 60")]
        public string Sted { get; set; }

        [Required(ErrorMessage = "Dato er påkrævet")]
        public DateTime Dato { get; set; }

        [Required(ErrorMessage = "Beskrivelse er påkrævet")]
        [StringLength(255, ErrorMessage = "Overskredet max karakter længde af 255")]
        public string Beskrivelse { get; set; }

        [Required(ErrorMessage = "MedlemMax er påkrævet")]
        public int MedlemMax { get; set; }

        public decimal? Pris { get; set; }
        #endregion

        #region Constructors
        //Empty
        public Begivenhed() { }

        //Full
        public Begivenhed(int eventId, string titel, string sted, DateTime dato, string beskrivelse, int medlemMax, decimal? pris)
        {
            EventId = eventId;
            Titel = titel;
            Sted = sted;
            Dato = dato;
            Beskrivelse = beskrivelse;
            MedlemMax = medlemMax;
            Pris = pris;
        }
        #endregion

        public override string ToString()
        {
            return $"Titel: {Titel}\n" +
                   $"Sted: {Sted}\n" +
                   $"Dato: {Dato}\n" +
                   $"Beskrivelse: {Beskrivelse}\n" +
                   $"Max antal medlemmer: {MedlemMax}\n" +
                   $"Pris pr. person {Pris} kr";
        }
    }
}
