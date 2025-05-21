namespace GadevangGruppe3Razor.Interfaces
{
    public interface ITilmeldHold
    {
        int BrugerId { get; set; }
        int HoldId { get; set; }
        string ToString();
    }
}
