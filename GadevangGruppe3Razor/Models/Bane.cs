using GadevangGruppe3Razor.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GadevangGruppe3Razor.Models
{
    public class Bane : IBane
    {
        private string _beskrivelse;

        public int BaneId { get; }

        [Required(ErrorMessage = "Banetype er påkrævet")]
        public BaneType Type { get; set; }

        [Required(ErrorMessage = "Banemiljø er påkrævet")]
        public BaneMiljø Miljø { get; set; }

        [Required(ErrorMessage = "Beskrivelse er påkrævet")]
        [StringLength(250, ErrorMessage = "Overskredet max karakter længde af 250")]
        public string Beskrivelse 
        { 
            get { return _beskrivelse; } 
            set { _beskrivelse=value; } 
        }

        public Bane()
        {
            
        }

        public Bane(int baneId, BaneType type, BaneMiljø miljø, string beskrivelse)
        {
            BaneId = baneId;
            Type = type;
            Miljø = miljø;
            Beskrivelse = beskrivelse;
        }

        public override string ToString()
        {
            return $"{Type}Bane {BaneId}: {_beskrivelse}\n" +
                   $"Miljø: {Miljø}\n";
        }
    }
}
