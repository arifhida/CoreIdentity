using CoreIdentity.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private IUserRepository _userRepository;
        private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;

        public AdminController(IUserRepository userRepository, IRoleRepository roleRepository,
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
        [HttpGet("GetUser",Name ="GetAllUser")]
        public async Task<IActionResult> Get()
        {
            var data = await _userRepository.GetAll();
            var json = JsonConvert.SerializeObject(data, _serializerSettings);
            return new OkObjectResult(json);
        }

        public async Task<IActionResult> PostRole()
        {
            return new OkObjectResult("");
        }

    }
}
