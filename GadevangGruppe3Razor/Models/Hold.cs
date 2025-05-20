using GadevangGruppe3Razor.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GadevangGruppe3Razor.Models
{
	public class Hold : IHold
	{
		[Required(ErrorMessage = "Hold Id kan ikke være null")]
		public int HoldId { get; set; }

		[Required(ErrorMessage = "Holdnavn kan ikke være null")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Holdnavnet kan ikke være kortere end 2 karaktere eller længere end 30 karaktere")]
		public string Holdnavn { get; set; }

		[Required(ErrorMessage = "Instruktørnavn kan ikke være null")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Instruktørnavn kan ikke være kortere end 2 karakterer eller længere end 30 karakterer")]
		public string Instruktørnavn { get; set; }

		[Required(ErrorMessage = "Start datoen kan ikke være null")]
		public DateOnly StartDato { get; set; }

		[Required(ErrorMessage = "Slut datoen kan ikke være null")]
		public DateOnly SlutDato { get; set; }

		[Required(ErrorMessage = "Tid kan ikke være null")]
		[StringLength(17, MinimumLength = 17, ErrorMessage = "Tid kan ikke være kortere eller længere end 17 karaktere")]
		public string Tid { get; set; }

		[Required(ErrorMessage = "Sted kan ikke være null")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Sted kan ikke være kortere end 2 karaktere eller længere end 50 karaktere")]
		public string Sted { get; set; }

		[Required(ErrorMessage = "Pris kan ikke være null")]
		public double Pris { get; set; }

		[Required(ErrorMessage = "Maximum medlemstal kan ikke være null")]
		public int MaxMedlemstal { get; set; }
		
		[Required(ErrorMessage = "Listen af tilmeldte brugere kan ikke være null")]
		public List<Bruger> TilmeldteBrugere { get; set; }

		public Hold()
		{
			TilmeldteBrugere = new List<Bruger>();
		}

        public Hold(int holdId, string holdnavn, string instruktørnavn, DateOnly startDato, DateOnly slutDato, string tid, string sted, double pris, int maxMedlemstal)
        {
			HoldId = holdId;
			Holdnavn = holdnavn;
			Instruktørnavn = instruktørnavn;
			StartDato = startDato;
			SlutDato = slutDato;
			Tid = tid;
			Sted = sted;
			Pris = pris;
			MaxMedlemstal = maxMedlemstal;
			TilmeldteBrugere = new List<Bruger>();
        }

		public override string ToString()
		{
			return $"Hold ID: {HoldId}, Holdnavn: {Holdnavn}, Instruktørnavn: {Instruktørnavn}, Start Dato: {StartDato}, Slut Dato: {SlutDato}, Tid: {Tid}, Sted: {Sted}, Pris: {Pris}, Max Medlemstal: {MaxMedlemstal}";
		}
	}
}
