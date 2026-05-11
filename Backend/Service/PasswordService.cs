namespace Backend.Service
{
    using Microsoft.AspNetCore.Identity;
    using Backend.Models;

    public class PasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(string password)
        {
            var dummyUser = new User();
            return _hasher.HashPassword(dummyUser, password);
        }

        public bool Verify(User user, string password, string hash)
        {
            var result = _hasher.VerifyHashedPassword(user, hash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}