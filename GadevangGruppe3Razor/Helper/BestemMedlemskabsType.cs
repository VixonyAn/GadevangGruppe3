using GadevangGruppe3Razor.Models;
namespace GadevangGruppe3Razor.Helper
{
	public class BestemMedlemskabsType
	{
		public int CalculateAlder(DateOnly fødselsdato)
		{
			DateOnly nuDato = DateOnly.FromDateTime(DateTime.Now);
			int alder = (nuDato.Year - fødselsdato.Year);

			return alder;
		}

		public MedlemskabsType BestemMedlemskabsTypeFraAlder(int alder)
		{
			MedlemskabsType medlemskabsType = MedlemskabsType.Juniorer;
			
			if (alder <= 18 && alder >= 25)
			{
				medlemskabsType = MedlemskabsType.Ung_Seniorer;
			}
			else if (alder > 25)
			{
				medlemskabsType = MedlemskabsType.Seniorer;
			}
			return medlemskabsType;
		}
	}
}
