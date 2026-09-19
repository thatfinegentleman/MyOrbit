namespace MyOrbit.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolTypeId { get; set; }
        public string FieldOfStudy { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
    }
}
