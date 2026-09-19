using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class InteractionPersonDto
    {
        [Required]
        public int PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? PersonSurname { get; set; }

        [Required]
        public int InteractionId { get; set; }
        public string? InteractionTitle { get; set; }
        public DateTime? InteractionDate { get; set; }
        public string? InteractionDescription { get; set; }

        public bool? IsPositiveInteraction { get; set; }
    }
}
