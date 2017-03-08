﻿using CoreIdentity.API.Model;
using CoreIdentity.API.Options;
using CoreIdentity.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace CoreIdentity.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private IUserRepository _userRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        private IUserInRoleRepository _userInRoleRepository;

        public TokenController(IOptions<JwtIssuerOptions> jwtOptions, IUserRepository userRepository,
            IUserInRoleRepository userInRoleRepository)
        {
            _jwtOptions = jwtOptions.Value;
            _userRepository = userRepository;
            _userInRoleRepository = userInRoleRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
       
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromForm] ApplicationUser applicationUser)
        {
            var userIdentity = await GetClaimIdentity(applicationUser);
            if (userIdentity == null)
            {
                return BadRequest("Invalid credentials");
            }
            var roles = await _userInRoleRepository.FindByAsync(x => x.User.UserName == applicationUser.Username);
            
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                    userIdentity.FindFirst("Username")                                     
                };
            foreach (var item in roles)
            {
                var claim = new Claim("Roles", item.Role.RoleName);
                claims.Append(claim);
            }
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalDays
            };
            var json = JsonConvert.SerializeObject(response, _serializerSettings);            
            return new OkObjectResult(json);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var list = await _userRepository.GetAll();
            return new OkObjectResult(list);
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private Task<ClaimsIdentity> GetClaimIdentity(ApplicationUser appUser)
        {
            var result = _userRepository.GetSingle(x => x.UserName == appUser.Username && x.Password == appUser.Password);
            if (result != null)
            {               
                
                return Task.FromResult(new ClaimsIdentity(
                    new GenericIdentity(appUser.Username, "Token"),
                    new[]
                    {
                        new Claim("Username",appUser.Username)
                    }
                    ));
            }
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }

}