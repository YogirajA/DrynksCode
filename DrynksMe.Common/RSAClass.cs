using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DrynksMe.Common
{
    public class RSAClass
    {  

        private static readonly UnicodeEncoding Encoder = new UnicodeEncoding();


        public static string Decrypt(string data)
        {
           
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new[] { ',' });
            var dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            var privateKey = ReadFile("PrivateKey.txt");
            rsa.FromXmlString(privateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return Encoder.GetString(decryptedByte);

        }

        public static string Encrypt(string data)
        {

            var rsa = new RSACryptoServiceProvider();
            var publicKey = ReadFile("PublicKey.txt");
            rsa.FromXmlString(publicKey);
            var dataToEncrypt = Encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();

        }

        public static string ReadFile(string fileName)
        {
            var filePath = Path.Combine(HttpContext.Current.Server.MapPath(@"~/App_Data"),fileName);
            var plainFile = File.OpenText(filePath);
            return plainFile.ReadToEnd();
        }


    }
}