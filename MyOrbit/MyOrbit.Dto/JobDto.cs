using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class JobDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il ruolo è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il ruolo non può superare i 100 caratteri.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Il nome del posto di lavoro è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome del posto di lavoro non può superare i 100 caratteri.")]
        public string WorkplaceName { get; set; }

        [Required(ErrorMessage = "Il luogo del posto di lavoro è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il luogo del posto di lavoro non può superare i 100 caratteri.")]
        public string Location { get; set; }
    }
}
