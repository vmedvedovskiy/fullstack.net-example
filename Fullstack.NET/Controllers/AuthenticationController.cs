﻿using System.Threading.Tasks;
using Fullstack.NET.Models;
using Fullstack.NET.Services.Authentication;
using Fullstack.NET.Services.Authentication.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.NET.Controllers
{
    [Route("api/v1/store/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUsersQueryService users;
        private readonly ITokenProvider tokens;

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(CredentialsModel credentials)
        {
            var user = await this.users
                .Find(credentials.Username, credentials.Password);

            if (user == null)
            {
                return this.Unauthorized();
            }

            return this.Ok(new
            {
                Token = this.tokens.Get(user)
            });
        }
    }
}
