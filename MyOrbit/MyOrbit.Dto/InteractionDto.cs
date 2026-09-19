using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class InteractionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo dell'interazione è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il titolo dell'interazione non può superare i 100 caratteri.")]
        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public bool? IsPositive { get; set; }
    }
}
