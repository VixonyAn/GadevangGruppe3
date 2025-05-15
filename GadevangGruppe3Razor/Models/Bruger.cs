using GadevangGruppe3Razor.Interfaces;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace GadevangGruppe3Razor.Models
{
    public class Bruger : IBruger
    {
        #region Properties
        [Required(ErrorMessage = "Bruger Id kan ikke være null")]
        public int BrugerId { get; set; }

        [Required(ErrorMessage = "Brugernavn kan ikke være null")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Brugernavn kan ikke være kortere end 2 karakterer længere end 30 karakterer")]
        public string Brugernavn { get; set; }

        [Required(ErrorMessage = "Adgangskode kan ikke være null")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Adgangskode kan ikke være korterer end 4 karakterer længere end 50 karakterer")]
        public string Adgangskode { get; set; }

        [Required(ErrorMessage = "Email kan ikke være null")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Email kan ikke være korterer end 4 karakterer længere end 50 karakterer")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefonnummer kan ikke være null")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Telefonnummer kan ikke være længere end 8 karakterer")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "MedlemskabsTypen kan ikke være null")]
        public MedlemskabsType Medlemskab { get; set; }

        [Required(ErrorMessage = "Positionen kan ikke være null")]
        public Position Positionen { get; set; }

        [Required(ErrorMessage = "Verificeringen kan ikke være null")]
        public bool Verificeret { get; set; }

        [StringLength(255, MinimumLength = 0, ErrorMessage = "Billed url kan ikke være længere end 255 karakterer")]
        public string BilledUrl { get; set; }
        #endregion

        #region Constructors
        public Bruger()
        {
            
        }

        public Bruger(string email, string adgangskode)
        {
            Email = email;
            Adgangskode = adgangskode;
        }

		public Bruger(int brugerID, string brugernavn, string adgangskode, string email, string telefon, string billedUrl, MedlemskabsType medlemskab, Position position, bool verificeret)
        {
			BrugerId = brugerID;
			Brugernavn = brugernavn;
			Adgangskode = adgangskode;
			Email = email;
            Telefon = telefon;
            BilledUrl = billedUrl;
            Medlemskab = medlemskab;
            Positionen = position;
            Verificeret = verificeret;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Bruger ID: {BrugerId}, Brugernavn: {Brugernavn}, Adgangskode: {Adgangskode}, Email: {Email}, Telefonnummer: {Telefon}, Medlemskab: {Medlemskab}, Position: {Positionen}, Verificeret: {Verificeret}";
        }
        #endregion
    }
}
