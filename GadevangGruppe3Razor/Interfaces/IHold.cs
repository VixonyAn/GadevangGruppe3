using GadevangGruppe3Razor.Models;

namespace GadevangGruppe3Razor.Interfaces
{
	public interface IHold
	{
		int HoldId { get; set; }
		string Holdnavn { get; set; }
		string Instruktørnavn { get; set; }
		DateOnly StartDato { get; set; }
		DateOnly SlutDato { get; set; }
		string Tid { get; set; }
		string Sted { get; set; }
		double Pris { get; set; }
		int MaxMedlemstal { get; set; }
		List<Bruger> TilmeldteBrugere { get; set; }
	}
}
