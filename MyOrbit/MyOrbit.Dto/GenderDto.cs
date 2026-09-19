using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class GenderDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Name { get; set; }
    }
}
