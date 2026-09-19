using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class PreferenceDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della preferenza è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La categoria della preferenza è obbligatoria.")]
        public int PreferenceCategoryId { get; set; }
        public string? PreferenceCategoryName { get; set; }
    }
}
