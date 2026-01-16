namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
    }
}
