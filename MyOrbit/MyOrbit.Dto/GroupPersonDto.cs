using System.ComponentModel.DataAnnotations;

namespace MyOrbit.Dto
{
    public class GroupPersonDto
    {
        [Required]
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupColorHex { get; set; }

        [Required]
        public int PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? PersonSurname { get; set; }
    }
}
