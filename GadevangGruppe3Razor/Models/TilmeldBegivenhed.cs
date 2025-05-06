using GadevangGruppe3Razor.Interfaces;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GadevangGruppe3Razor.Models
{
    public class TilmeldBegivenhed : ITilmeldBegivenhed
    {
        #region Properties
        [Required(ErrorMessage = "BrugerId er påkrævet")]
        public int BrugerId { get; set; }
        
        [Required(ErrorMessage = "EventId er påkrævet")]
        public int EventId { get; set; }

        [StringLength(255, ErrorMessage = "Overskredet max karakter længde af 255")]
        public string Kommentar { get; set; }
        #endregion

        #region Constructors
        //Empty
        public TilmeldBegivenhed() { }

        //Full
        public TilmeldBegivenhed(int brugerId, int eventId, string kommentar)
        {
            BrugerId = brugerId;
            EventId = eventId;
            Kommentar = kommentar;
        }
        #endregion

        public override string ToString()
        {
            return $"BrugerId: #{BrugerId} - EventId : #{EventId}\n" +
                   $"Kommentar: {Kommentar}";
        }
    }
}
