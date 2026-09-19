using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class SexualOrientationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome dell'orientamento sessuale è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome dell'orientamento sessuale non può superare i 50 caratteri.")]
        public string Name { get; set; }
    }
}
