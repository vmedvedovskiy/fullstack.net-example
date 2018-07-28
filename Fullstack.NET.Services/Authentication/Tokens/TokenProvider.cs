using System;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Extensions.Options;

namespace Fullstack.NET.Services.Authentication.Tokens
{
    public class TokenProvider : ITokenProvider, IDisposable
    {
        private const bool DoOAEPPadding = false;

        private readonly RSACryptoServiceProvider cryptoAlgorithm 
            = new RSACryptoServiceProvider();

        private readonly DataContractJsonSerializer serializer
            = new DataContractJsonSerializer(typeof(TokenData));

        public TokenProvider(IOptions<TokenOptions> opts)
            => this.cryptoAlgorithm.ImportParameters(this.FromOptions(opts.Value));

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

                var encryptedToken = this.cryptoAlgorithm.Encrypt(
                    stream.ToArray(),
                    DoOAEPPadding);

                return Convert.ToBase64String(encryptedToken);
            }
        }   

        public bool IsValid(string token)
        {
            var decryptedToken = this.cryptoAlgorithm.Decrypt(
                    Convert.FromBase64String(token),
                    DoOAEPPadding);

            using (var stream = new MemoryStream())
            {
                var deserializedData = (TokenData)serializer.ReadObject(stream);

                return DateTime.FromFileTimeUtc(deserializedData.TodayEpochSeconds)
                    == DateTime.Today;
            }
        }

        public void Dispose() => this.cryptoAlgorithm?.Dispose();

        private RSAParameters FromOptions(TokenOptions opts)
        {
            return new RSAParameters
            {
                D = Convert.FromBase64String(opts.D),
                DP = Convert.FromBase64String(opts.DP),
                DQ = Convert.FromBase64String(opts.DQ),
                Exponent = Convert.FromBase64String(opts.Exponent),
                InverseQ = Convert.FromBase64String(opts.InverseQ),
                Modulus = Convert.FromBase64String(opts.Modulus),
                P = Convert.FromBase64String(opts.P),
                Q = Convert.FromBase64String(opts.Q)
            };
        }

        [DataContract]
        private class TokenData
        {
            [DataMember]
            public string Username { get; set; }

            [DataMember]
            public long TodayEpochSeconds { get; set; }
        }
    }
}
