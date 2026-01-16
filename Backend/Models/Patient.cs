namespace Backend.Models
{
    public class Patient
    {
        public int Id { get; set; }  // Primary Key
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}