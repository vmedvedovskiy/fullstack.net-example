using System;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Fullstack.NET.Services.Authentication
{
    public class TokenProvider : ITokenProvider
    {
        private readonly RSACryptoServiceProvider cryptoAlgorithm 
            = new RSACryptoServiceProvider();

        private readonly DataContractJsonSerializer serializer
            = new DataContractJsonSerializer(typeof(TokenData));

        private readonly RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA512;

        public string Get(UserModel user)
        {
            using (var stream = new MemoryStream())
            {
                var tokenData = new TokenData
                {
                    TodayEpochSeconds = DateTime.Today.ToFileTimeUtc(),
                    Username = user.Username
                };

                serializer.WriteObject(stream, tokenData);

                var encryptedToken = cryptoAlgorithm.Encrypt(
                    stream.ToArray(),
                    padding);

                return Encoding.UTF8.GetString(encryptedToken);
            }
        }   

        public bool IsValid(string token)
        {
            var decryptedToken = cryptoAlgorithm.Decrypt(
                    Encoding.UTF8.GetBytes(token),
                    padding);

            using (var stream = new MemoryStream())
            {
                var deserializedData = (TokenData)serializer.ReadObject(stream);

                return DateTime.FromFileTimeUtc(deserializedData.TodayEpochSeconds)
                    == DateTime.Today;
            }
        }

        private class TokenData
        {
            public string Username { get; set; }

            public long TodayEpochSeconds { get; set; }
        }
    }
}
