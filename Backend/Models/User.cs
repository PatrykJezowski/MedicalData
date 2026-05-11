namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }  // PK

        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Pesel { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
    