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

        public AuthenticationController(
            IUsersQueryService users, 
            ITokenProvider tokens)
        {
            this.users = users;
            this.tokens = tokens;
        }

        [Route("user/find")]
        [HttpGet]
        public async Task<IActionResult> Find(string phoneNumber = "")
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
            {
                return this.BadRequest();
            }

            var user = await this.users.Find(phoneNumber);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CredentialsModel credentials)
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
