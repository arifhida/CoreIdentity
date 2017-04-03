using AutoMapper;
using CoreIdentity.API.Model;
using CoreIdentity.Data.Abstract;
using CoreIdentity.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private IUserRepository _userRepository;
        private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        private ICategoryRepository _categoryRepository;

        public CategoryController(IUserRepository userRepository, IRoleRepository roleRepository,
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

        [HttpPost("GetAllCategory", Name = "GetCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var data = await _categoryRepository.FindByAsyncIncluding(x => x.ParentId == null, x => x.SubCategory);
            var result = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(data);
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpPost("AddCategory", Name = "NewCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCategory([FromBody] CategoryViewModel category)
        {
            var _newCategory = Mapper.Map<CategoryViewModel, Category>(category);            
            _categoryRepository.Add(_newCategory);
            await _categoryRepository.Commit();
            return new NoContentResult();

        }
    }
}
