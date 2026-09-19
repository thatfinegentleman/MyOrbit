using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Il cognome non può superare i 50 caratteri.")]
        public string? Surname { get; set; }

        [StringLength(50, ErrorMessage = "Il soprannome non può superare i 50 caratteri.")]
        public string? Nickname { get; set; }

        public DateTime? BirthDate { get; set; }

        [Range(1900, 2100, ErrorMessage = "Inserisci un anno di nascita valido.")]
        public int? BirthYear { get; set; }

        [StringLength(500, ErrorMessage = "Il link dell'immagine del profilo non può superare i 500 caratteri.")]
        public string? ProfilePicture { get; set; }

        [Required(ErrorMessage = "Il genere è obbligatorio.")]
        public int GenderId { get; set; }
        public string? GenderName { get; set; }

        [Required(ErrorMessage = "L'orientamento sessuale è obbligatorio.")]
        public int SexualOrientationId { get; set; }
        public string? SexualOrientationName { get; set; }

        public int? NationalityId { get; set; }
        public string? NationalityName { get; set; }
        public string? NationalityFlag { get; set; }

        [StringLength(500, ErrorMessage = "L'indirizzo della casa 1 non può superare i 500 caratteri.")]
        public string? HomeAddress1 { get; set; }

        [StringLength(500, ErrorMessage = "L'indirizzo della casa 2 non può superare i 500 caratteri.")]
        public string? HomeAddress2 { get; set; }

        [Required(ErrorMessage = "Il tipo di relazione è obbligatorio.")]
        public int RelationshipId { get; set; }
        public string? RelationshipName { get; set; }
        public string? RelationshipColorHex { get; set; }

        public int? SchoolId { get; set; }
        public string? SchoolName { get; set; }
        public string? SchoolType { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? SchoolLocation { get; set; }

        public int? JobId { get; set; }
        public string? JobRole { get; set; }
        public string? JobWorkplaceName { get; set; }
        public string? JobLocation { get; set; }

        public string? Notes { get; set; }

        public int ProfileClicks { get; set; }
    }
}
