﻿using eProdaja.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace eProdaja.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public IKorisniciService KorisniciService { get; set; }
        public BasicAuthenticationHandler(IKorisniciService korisniciService, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            KorisniciService = korisniciService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing auth header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]); // token iz hedera
            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter); //nas unoc
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':');//encode, splitovana linija iznad sa dvotackom

            var username = credentials[0];
            var password = credentials[1];

            var user = KorisniciService.Login(username, password);

            if (user == null)
            {
                return AuthenticateResult.Fail("Incorrect username or password");
            }

            //we claim something...
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,username),
                new Claim(ClaimTypes.Name, user.Ime)
            };


            foreach (var role in user.KorisniciUloges)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Uloga.Naziv));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);

            //compatibility shit.
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
