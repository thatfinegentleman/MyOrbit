using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class SchoolTypeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del tipo di scuola è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Name { get; set; }
    }
}
