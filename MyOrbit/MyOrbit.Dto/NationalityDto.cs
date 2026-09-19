using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class NationalityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La bandiera è obbligatoria.")]
        [StringLength(10, ErrorMessage = "La bandiera non può superare i 10 caratteri.")]
        public string Flag { get; set; }
    }
}
