using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class PreferenceCategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della categoria è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome della categoria non può superare i 50 caratteri.")]
        public string Name { get; set; }
    }
}
