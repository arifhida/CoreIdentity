using AutoMapper;
using CoreIdentity.API.Model;
using CoreIdentity.Data.Abstract;
using CoreIdentity.Model.Entities;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization;
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
        //private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;

        public AdminController(IUserRepository userRepository, IRoleRepository roleRepository)
            //IUserInRoleRepository userInRoleRepository)
        {
            _roleRepository = roleRepository;
            //_userInRoleRepository = userInRoleRepository;
            _userRepository = userRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        [HttpGet("GetUser",Name ="GetAllUser")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Get()
        {
            var data = await _userRepository.GetAll();
            var json = JsonConvert.SerializeObject(data, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpGet("GetUserById",Name ="GetUserByName")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var user = await _userRepository.GetSingleAsync(x => x.Id == Id, r => r.UserRole);
            var listRole = new List<UserInRole>();
            foreach (var item in user.UserRole)
            {
                var role = _roleRepository.GetSingle(item.RoleId);
                item.Role = role;
                
            }
            var _result = Mapper.Map<User, UserViewModel>(user);            
            var json = JsonConvert.SerializeObject(_result, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpPost("AddRole",Name ="AddNewRole")]
        public async Task<IActionResult> PostRole([FromForm] RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Role _newRole = Mapper.Map<RoleViewModel, Role>(role);
            _roleRepository.Add(_newRole);
            await _roleRepository.Commit();
            var json = JsonConvert.SerializeObject(role, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpPost("AddUser", Name = "AddNewUser")]
        public async Task<IActionResult> AddUser([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User _newUser = Mapper.Map<UserViewModel, User>(user);
            var _roles = Mapper.Map<IEnumerable<RoleViewModel>, IEnumerable<Role>>(user.Roles);
            var _userinRoles = new List<UserInRole>();
            foreach (var item in _roles)
            {
                var _userinrole = new UserInRole();
                //_userinrole.Role = item;
                _userinrole.RoleId = item.Id;
                _userinrole.User = _newUser;
                _newUser.UserRole.Add(_userinrole);
            }
            //_newUser.UserRole = _userinRoles;
            _userRepository.Add(_newUser);
            await _userRepository.Commit();
            var json = JsonConvert.SerializeObject(user, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpPost("UpdateUser", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User _newUser = Mapper.Map<UserViewModel, User>(user);
            var _roles = Mapper.Map<IEnumerable<RoleViewModel>, IEnumerable<Role>>(user.Roles);
            var _userinRoles = new List<UserInRole>();
            foreach (var item in _roles)
            {
                var _userinrole = new UserInRole();
                _userinrole.RoleId = item.Id;
                _userinrole.UserId = _newUser.Id;
                _userinrole.User = _newUser;
                _newUser.UserRole.Add(_userinrole);

            }
             
            _userRepository.Update(_newUser);
            await _userRepository.Commit();

            var json = JsonConvert.SerializeObject(user, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpPost("UpdateUserSync", Name = "UpdateUserSync")]
        public IActionResult UpdateUserSync([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User _newUser = Mapper.Map<UserViewModel, User>(user);
            var _roles = Mapper.Map<IEnumerable<RoleViewModel>, IEnumerable<Role>>(user.Roles);
            var _userinRoles = new List<UserInRole>();
            foreach (var item in _roles)
            {
                var _userinrole = new UserInRole();
                _userinrole.RoleId = item.Id;
                _userinrole.UserId = _newUser.Id;
                _userinrole.User = _newUser;
                _newUser.UserRole.Add(_userinrole);

            }

            var existingUser = _userRepository.GetSingle(x => x.Id == user.Id, r => r.UserRole);
             
            foreach (var item in existingUser.UserRole)
            {
                if (_newUser.UserRole.Where(x => x.RoleId == item.RoleId).Count() < 1)
                {
                    _userinRoles.Add(item);
                }
            }
            foreach (var item in _newUser.UserRole)
            {
                //var isExist = existingUser.UserRole.Where(x => x.RoleId == item.RoleId);
                if (existingUser.UserRole.Where(x=> x.RoleId == item.RoleId).Count() < 1)
                    existingUser.UserRole.Add(item);
            }
            foreach (var item in _userinRoles)
            {
                
                existingUser.UserRole.Remove(item);
            }
            
            _userRepository.CommitSync(existingUser);
            var json = JsonConvert.SerializeObject(user, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpGet("GetAllRole",Name ="AllRole")]
        public async Task<IActionResult> GetAllRoles()
        {
            var data = await _roleRepository.AllIncluding(x => x.UserInRole);
            var result = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(data);
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }
        //[HttpGet("GetUserInRole", Name = "UserInRole")]
        //public async Task<IActionResult> GetAllUserInRole()
        //{
        //    //var data = await _userInRoleRepository.FindByAsyncIncluding(l=> l.User.UserName== "arif.hidayat", x => x.Role, r => r.User);
        //    return new OkObjectResult("");
        //}

    }
}
