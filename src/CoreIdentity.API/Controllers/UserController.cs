using AutoMapper;
using CoreIdentity.API.Model;
using CoreIdentity.API.Options;
using CoreIdentity.Data.Abstract;
using CoreIdentity.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        int page = 1;
        int pageSize = 10;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository,
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

        [HttpGet("GetById", Name = "GetUserById")]
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

        [HttpPost("UpdateUser", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User _newUser = Mapper.Map<UserViewModel, User>(user);
            var _roles = Mapper.Map<IEnumerable<UserInRoleViewModel>, IEnumerable<UserInRole>>(user.Roles);
            var _userinRoles = new List<UserInRole>();
            foreach (var item in _roles)
            {
                item.UserId = _newUser.Id;
                item.User = _newUser;
                _newUser.UserRole.Add(item);

            }

            _userRepository.Update(_newUser, excludeProperties: "Password,UserName,Salt,isActive");
            await _userRepository.Commit();
            var json = JsonConvert.SerializeObject(user, _serializerSettings);
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
            _newUser.Salt = StringHash.SaltGen();
            _newUser.Password = StringHash.GetHash(_newUser.Password + _newUser.Salt);
            var _roles = Mapper.Map<IEnumerable<UserInRoleViewModel>, IEnumerable<UserInRole>>(user.Roles);
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
            var createdUser = Mapper.Map<User, UserViewModel>(_newUser);
            var json = JsonConvert.SerializeObject(createdUser, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpGet("GetData",Name ="GetUsers")]
        public async Task<IActionResult> Get()
        {
            var pagination = Request.Headers["Pagination"];
            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }
            var q = Request.Headers["q"].ToString();
            int currentPage = page;
            int currentPageSize = pageSize;
            var data = await _userRepository.FindByAsync(x => x.UserName.Contains(q) || x.FullName.Contains(q) || x.Email.Contains(q));
            var totalData = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalData / pageSize);
            Response.AddPagination(page, pageSize, totalData, totalPages);
            data.Skip(page * pageSize).Take(pageSize);

            var _result = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(data);
            var json = JsonConvert.SerializeObject(_result, _serializerSettings);
            return new OkObjectResult(json);
        }
    }
}
