namespace MyOrbit.Models
{
    public class Preference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PreferenceCategoryId { get; set; }
        public string UserId { get; set; }
    }
}
