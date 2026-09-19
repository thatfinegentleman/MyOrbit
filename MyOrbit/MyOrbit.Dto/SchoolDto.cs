using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class SchoolDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della scuola è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome della scuolanon può superare i 100 caratteri.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il tipo di scuola è obbligatorio.")]
        public int SchoolTypeId { get; set; }
        public string? SchoolTypeName { get; set; }

        [Required(ErrorMessage = "L'ambito di studio è obbligatorio.")]
        [StringLength(100, ErrorMessage = "L'ambito di studio non può superare i 100 caratteri.")]
        public string FieldOfStudy { get; set; }

        [Required(ErrorMessage = "Il luogo dove si trova la scuola è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome del luogo non può superare i 100 caratteri.")]
        public string Location { get; set; }
    }
}
