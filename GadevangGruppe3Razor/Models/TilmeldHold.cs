using GadevangGruppe3Razor.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GadevangGruppe3Razor.Models
{
    public class TilmeldHold : ITilmeldHold
    {
        #region Properties
        [Required(ErrorMessage = "BrugerId er påkrævet")]
        public int BrugerId { get; set; }

        [Required(ErrorMessage = "HoldId er påkrævet")]
        public int HoldId { get; set; }
        #endregion

        #region Constructors
        //Empty
        public TilmeldHold() { }

        //Full
        public TilmeldHold(int brugerId, int holdId)
        {
            BrugerId = brugerId;
            HoldId = holdId;
        }
        #endregion

        public override string ToString()
        {
            return $"BrugerId: #{BrugerId} - HoldId : #{HoldId}";
        }
    }
}
