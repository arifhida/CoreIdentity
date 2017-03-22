using CoreIdentity.API.Model;
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
        //private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;

        public TokenController(IOptions<JwtIssuerOptions> jwtOptions, IUserRepository userRepository,
             IRoleRepository roleRepository)
        {
            _jwtOptions = jwtOptions.Value;
            _userRepository = userRepository;
            
            _roleRepository = roleRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
       
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] ApplicationUser applicationUser)
        {
            var userIdentity = await GetClaimIdentity(applicationUser);
            if (userIdentity == null)
            {                
                return BadRequest("invalid credential");
            }
            var roles = await _userRepository.GetSingleAsync(x => x.UserName == applicationUser.Username && x.Password == applicationUser.Password,
                y=> y.UserRole);
            var roleClaims = new List<Claim>();
            foreach (var item in roles.UserRole)
            {
                var role = _roleRepository.GetSingle(item.RoleId);
                roleClaims.Add(new Claim("Roles", role.RoleName));
            }            
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                    userIdentity.FindFirst("Username")                                                      
                };

            var claimlist = claims.ToList();
            claimlist.AddRange(roleClaims);

            
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claimlist.AsEnumerable(),
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
                var identity = new ClaimsIdentity(
                    new GenericIdentity(appUser.Username, "Token"),
                    new[]
                    {
                        new Claim("Username",appUser.Username)                             

                    }
                    );
               
                return Task.FromResult(identity);
            }
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }

}
