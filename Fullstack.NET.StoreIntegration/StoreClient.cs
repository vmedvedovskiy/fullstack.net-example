using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Fullstack.NET.StoreIntegration.Contract;
using Newtonsoft.Json;
using Optional;

namespace Fullstack.NET.StoreIntegration
{
    public class StoreClient
    {
        public async Task<Option<User>> FindUser(string phoneNumber)
        {
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync(
                    $"api/v1/store/auth/user/find?phoneNumber={WebUtility.UrlEncode(phoneNumber)}");

                if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Option.None<User>();
                }

                var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

                return Option.Some(user);
            }
        }

        public async Task<Option<IReadOnlyList<Order>>> GetUserOrders(Guid userId)
        {
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/v1/store/users/{userId}/orders");

                if(response.IsSuccessStatusCode)
                {
                    return Option.None<IReadOnlyList<Order>>();
                }

                var orders = JsonConvert.DeserializeObject<List<Order>>(
                    await response.Content.ReadAsStringAsync());

                return Option.Some((IReadOnlyList<Order>)orders);
            }
        }

        public Task<Option<object>> SubmitVerificationCode(string phoneNumber, string code)
        {
            int intCode;

            if (int.TryParse(code, out intCode))
            {
                return Task.FromResult(Option.Some(new object()));
            }
            else
            {
                return Task.FromResult(Option.None<object>());
            }
        }

        private static HttpClient CreateClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri("http://localhost:17495")
            };
        }
    }
}
