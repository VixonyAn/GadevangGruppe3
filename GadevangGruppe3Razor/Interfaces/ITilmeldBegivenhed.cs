namespace GadevangGruppe3Razor.Interfaces
{
    public interface ITilmeldBegivenhed
    {
        int BrugerId { get; set; }
        int EventId { get; set; }
        string Kommentar { get; set; }
        string ToString();
    }
}
