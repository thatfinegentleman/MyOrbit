using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class GroupDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome del gruppo è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il colore è obbligatorio.")]
        [StringLength(7, ErrorMessage = "Il colore deve essere un esadecimale di 7 caratteri.")]
        public string ColorHex { get; set; }
    }
}
