﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fullstack.NET.Services.Authentication.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fullstack.NET.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticateAttribute : TypeFilterAttribute
    {
        public AuthenticateAttribute(): base(typeof(AuthorizeActionFilter))
        {
        }
    }

    public class AuthorizeActionFilter : IAsyncActionFilter
    {
        private readonly ITokenProvider tokenProvider;

        public AuthorizeActionFilter(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, 
            ActionExecutionDelegate next)
        {
            var needToSkipAuth = context.ActionDescriptor
                .FilterDescriptors
                .Any(_ => _.Filter is AllowAnonymousFilter);

            if (needToSkipAuth)
            {
                await next();
            }

            var token = context.HttpContext
                .Request
                .Headers[HttpRequestHeader.Authorization.ToString()]
                .ToArray()
                .FirstOrDefault();

            bool isAuthorized = 
                !string.IsNullOrWhiteSpace(token) 
                && this.tokenProvider.IsValid(token);

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                await next();
            }
        }
    }

}
