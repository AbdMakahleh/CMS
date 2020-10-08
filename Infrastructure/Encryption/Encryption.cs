using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Encryption
{
  
    public class Encryption
    {

        private static Encryption _instance;
        public Encryption()
        {
            _key = "ASSWWRTnb809#$00";
        }

        public static Encryption Instance
        {
            get
            {
                if (_instance == default(Encryption))
                {
                    _instance = new Encryption();
                }
                return _instance;
            }
        }

        private string _key { get; set; }

        public string Decrypt(string cipherText, int counter = 3)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            var result = streamReader.ReadToEnd();
                            if (counter > 0)
                            {
                                return Decrypt(result, counter - 1);
                            }
                            return result;
                        }
                    }
                }
            }
        }

        public string Encrypt(string plainText, int counter = 3)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            if (counter > 0)
            {
                return Encrypt(Convert.ToBase64String(array), counter - 1);
            }

            return Convert.ToBase64String(array);
        }
    }
}
