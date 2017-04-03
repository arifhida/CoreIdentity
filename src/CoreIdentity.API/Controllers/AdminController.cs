using AutoMapper;
using CoreIdentity.API.Model;
using CoreIdentity.API.Options;
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
        private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        private ICategoryRepository _categoryRepository;

        public AdminController(IUserRepository userRepository, IRoleRepository roleRepository,
            IUserInRoleRepository userInRoleRepository, ICategoryRepository categoryRepository)
        {
            _roleRepository = roleRepository;
            _userInRoleRepository = userInRoleRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
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
        
        
        [HttpGet("GetAllRole",Name ="AllRole")]
        public async Task<IActionResult> GetAllRoles()
        {
            var data = await _roleRepository.AllIncluding(x => x.UserInRole);
            var result = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(data);
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpPost("GetRoleByName",Name ="RoleByName")]
        public async Task<IActionResult> GetRoleByName([FromBody]string role)
        {
            var roles = await _roleRepository.FindByAsync(x => x.RoleName == role);
            var exist = roles.Count() > 0;
            if (exist)
            {
                return BadRequest("role name is exist");
            }
            var result = new
            {
                Message = "rolename available"
            };
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpPost("GetUserName", Name = "GetUserName")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckUserName([FromBody]string username)
        {
            var _users = await _userRepository.FindByAsync(x => x.UserName == username);
            var exist = _users.Count() > 0;
            if (exist)
            {
                return BadRequest("Username is already exist");
            }
            var result = new
            {
                Message = "user available"
            };
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpPost("GetUserEmail", Name = "GetUserEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckEmail([FromBody]string email)
        {
            var _users = await _userRepository.FindByAsync(x => x.Email == email);
            var exist = _users.Count() > 0;
            if (exist)
            {
                return BadRequest("email is already exist");
            }
            var result = new
            {
                Message = "email available"
            };
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpPost("AddCategory",Name ="NewCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCategory()
        {
            var category = new Category { CategoryName = "Home", CategoryDescription = "Menu for home" };
            var child = new Category { CategoryName = "test", CategoryDescription = "desc" };
            category.SubCategory.Add(child);
            _categoryRepository.Add(category);
            await _categoryRepository.Commit();
            return new NoContentResult();
            
        }
        [HttpPost("GetCategory", Name = "GetCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory()
        {
            var data = await _categoryRepository.FindByAsyncIncluding(x=> x.ParentId == null,x => x.SubCategory);
            var result = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(data);
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }
    }
}
