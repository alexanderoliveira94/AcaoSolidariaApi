using System;
using System.Security.Cryptography;
using AcaoSolidariaApi.Models;

namespace AcaoSolidariaApi.Utils
{
    public class Criptografia
    {
        public static void CriarPasswordHash(Usuario usuario, string password)
        {
            using (var hmac = new HMACSHA512())
            {
                usuario.PasswordSalt = hmac.Key;
                usuario.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void CriarPasswordHash(ONG ong, string password)
        {
            using (var hmac = new HMACSHA512())
            {
                ong.PasswordSalt = hmac.Key;
                ong.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerificarPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
