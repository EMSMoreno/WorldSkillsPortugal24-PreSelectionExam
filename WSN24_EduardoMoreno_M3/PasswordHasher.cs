using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    public static string HashPassword(string password, out string salt)
    {
        // Gerar um salt aleatório
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }

        // Hashear a senha com o salt
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
        {
            byte[] hashBytes = pbkdf2.GetBytes(20);

            // Combinar o salt e o hash
            byte[] hashWithSaltBytes = new byte[36];
            Array.Copy(saltBytes, 0, hashWithSaltBytes, 0, 16);
            Array.Copy(hashBytes, 0, hashWithSaltBytes, 16, 20);

            // Converter para string Base64
            string hashWithSaltBase64 = Convert.ToBase64String(hashWithSaltBytes);

            // Retornar o salt também como string Base64 para armazenar separadamente se necessário
            salt = Convert.ToBase64String(saltBytes);

            return hashWithSaltBase64;
        }
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        // Extrair o salt do hash armazenado
        byte[] hashWithSaltBytes = Convert.FromBase64String(storedHash);
        byte[] saltBytes = new byte[16];
        Array.Copy(hashWithSaltBytes, 0, saltBytes, 0, 16);

        // Hashear a senha fornecida com o salt extraído
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
        {
            byte[] hashBytes = pbkdf2.GetBytes(20);

            // Comparar o hash gerado com o hash armazenado
            for (int i = 0; i < 20; i++)
            {
                if (hashWithSaltBytes[i + 16] != hashBytes[i])
                    return false;
            }

            return true;
        }
    }


}
