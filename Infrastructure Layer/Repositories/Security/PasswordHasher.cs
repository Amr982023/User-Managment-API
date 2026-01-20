using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Interfaces.Security;

namespace Infrastructure_Layer.Repositories.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32;    
        private const int Iterations = 10000;

        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

        public string Hash(string password)
        {
            
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

          
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithm,
                KeySize);

           
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string hashString)
        {
            
            var parts = hashString.Split('.');
            if (parts.Length != 3)
                return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] hash = Convert.FromBase64String(parts[2]);

            
            byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithm,
                hash.Length);

          
            return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
        }
    }
}
