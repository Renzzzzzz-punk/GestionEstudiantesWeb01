using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GestionEstudiantesDtos.Utilidades
{
    public static class CifradoAES
    {
        private static readonly string Clave = "12345678901234567890123456789012"; // 32 chars
        private static readonly string IV = "1234567890123456"; // 16 chars

        public static string Encriptar(string textoPlano)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Clave);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(textoPlano);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Desencriptar(string textoCifrado)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Clave);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(textoCifrado)))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}