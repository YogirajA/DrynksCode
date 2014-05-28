using System;
using System.IO;
using System.Security.Cryptography;
using DrynksMe.Common;
using DrynksMe.Services.Api.Models;
using Newtonsoft.Json;

namespace DrynksMe.SecurityHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateEncyptedHeaderFile();
        }

        private static void CreateEncyptedHeaderFile()
        {
            var apiheader = new DrynksApiHeader {ApiKey = "Blah", DeviceId = "1111"};
            var json = JsonConvert.SerializeObject(apiheader);
            var result = RSAClass.Encrypt(json);
            var publicKeyFile = File.CreateText("EncryptedHeader");
            publicKeyFile.Write(result);
        }

        static void CreateKeys(string publicKeyFileName, string privateKeyFileName)
        {
            // Variables
            StreamWriter publicKeyFile = null;
            StreamWriter privateKeyFile = null;

            try
            {
                // Create a new key pair on target CSP
                var cspParams = new CspParameters();

                cspParams.ProviderType = 1; // PROV_RSA_FULL 
                //cspParams.ProviderName; // CSP name
                cspParams.Flags = CspProviderFlags.UseArchivableKey;
                cspParams.KeyNumber = (int)KeyNumber.Exchange;


                var rsaProvider = new RSACryptoServiceProvider(cspParams);


                // Export public key
                string publicKey = rsaProvider.ToXmlString(false);

                // Write public key to file
                publicKeyFile = File.CreateText(publicKeyFileName);
                publicKeyFile.Write(publicKey);

                // Export private/public key pair 
                string privateKey = rsaProvider.ToXmlString(true);

                // Write private/public key pair to file
                privateKeyFile = File.CreateText(privateKeyFileName);
                privateKeyFile.Write(privateKey);
            }
            catch (Exception ex)
            {
                // Any errors? Show them
                Console.WriteLine("Exception generating a new key pair! More info:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Do some clean up if needed
                if (publicKeyFile != null)
                {
                    publicKeyFile.Close();
                }
                if (privateKeyFile != null)
                {
                    privateKeyFile.Close();
                }
            }

        } // Keys
    }
}
