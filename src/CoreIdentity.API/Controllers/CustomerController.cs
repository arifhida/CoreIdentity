using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreIdentity.API.Model;
using CoreIdentity.Data.Abstract;
using Newtonsoft.Json;
using CoreIdentity.Model.Entities;
using AutoMapper;
using CoreIdentity.API.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreIdentity.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private IUserRepository _userRepository;
        private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;

        public CustomerController(IUserRepository userRepository, IRoleRepository roleRepository,
            IUserInRoleRepository userInRoleRepository)
        {
            _roleRepository = roleRepository;
            _userInRoleRepository = userInRoleRepository;
            _userRepository = userRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        [HttpPost("Register",Name ="Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User _newUser = Mapper.Map<UserViewModel, User>(user);
            _newUser.Salt = StringHash.SaltGen();
            _newUser.Password = StringHash.GetHash(_newUser.Password + _newUser.Salt);                  
            _userRepository.Add(_newUser);
            await _userRepository.Commit();
            var result = new
            {
                Status = "Registration Success"
            };
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }
        
    }
}
