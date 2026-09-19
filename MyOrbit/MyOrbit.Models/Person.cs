namespace MyOrbit.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Nickname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? BirthYear { get; set; }
        public string? ProfilePicture { get; set; }
        public int GenderId { get; set; }
        public int SexualOrientationId { get; set; }
        public int? NationalityId { get; set; }
        public string? HomeAddress1 { get; set; }
        public string? HomeAddress2 { get; set; }
        public int RelationshipId { get; set; }
        public int? SchoolId { get; set; }
        public int? JobId { get; set; }
        public string? Notes { get; set; }
        public int ProfileClicks { get; set; }
        public string UserId { get; set; }
    }
}
