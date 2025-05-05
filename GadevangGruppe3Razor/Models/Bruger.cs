using GadevangGruppe3Razor.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GadevangGruppe3Razor.Models
{
    public class Bruger : IBruger
    {
        [Required(ErrorMessage = "Bruger Id kan ikke være null")]
        public int BrugerId { get; set; }

        [Required(ErrorMessage = "Brugernavn kan ikke være null")]
        [StringLength(30, ErrorMessage = "Brugernavn kan ikke være længere end 30 karakterer")]
        public string Brugernavn { get; set; }

        [Required(ErrorMessage = "Adgangskode kan ikke være null")]
        [StringLength(30, ErrorMessage = "Adgangskode kan ikke være længere end 50 karakterer")]
        public string Adgangskode { get; set; }

        [Required(ErrorMessage = "Email kan ikke være null")]
        [StringLength(30, ErrorMessage = "Email kan ikke være længere end 50 karakterer")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefonnummer kan ikke være null")]
        [StringLength(30, ErrorMessage = "Telefonnummer kan ikke være længere end 8 karakterer")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "MedlemskabsTypen kan ikke være null")]
        public MedlemskabsType Medlemskab { get; set; }

        [Required(ErrorMessage = "Positionen kan ikke være null")]
        public Position Positionen { get; set; }

        [Required(ErrorMessage = "Verificeringen kan ikke være null")]
        public bool Verificeret { get; set; }

        public Bruger()
        {
            
        }

        public Bruger(int brugerID, string brugernavn, string adgangskode, string email, string telefon, MedlemskabsType medlemskab, Position position, bool verificeret)
        {
            BrugerId = brugerID;
            Brugernavn = brugernavn;
            Adgangskode = adgangskode;
            Email = email;
            Telefon = telefon;
            //BilledUrl = billedUrl
            Medlemskab = medlemskab;
            Positionen = position;
            Verificeret = verificeret;
        }

        public override string ToString()
        {
            return $"Bruger ID: {BrugerId}, Brugernavn: {Brugernavn}, Adgangskode: {Adgangskode}, Email: {Email}, Telefonnummer: {Telefon}, Medlemskab: {Medlemskab}, Position: {Positionen}, Verificeret: {Verificeret}";
        }
    }
}
