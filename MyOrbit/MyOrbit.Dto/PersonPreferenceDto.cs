using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class PersonPreferenceDto
    {
        [Required]
        public int PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? PersonSurname { get; set; }

        [Required]
        public int PreferenceId { get; set; }
        public string? PreferenceName { get; set; }
        public string? PreferenceCategoryName { get; set; }

        [Required]
        public bool Likes { get; set; }
    }
}
