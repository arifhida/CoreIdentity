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
    public class RoleController : Controller
    {
        private IUserRepository _userRepository;
        private IUserInRoleRepository _userInRoleRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        int page = 1;
        int pageSize = 10;
        public RoleController(IUserRepository userRepository, IRoleRepository roleRepository,
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

        [HttpPost("AddRole", Name = "AddNewRole")]
        public async Task<IActionResult> PostRole([FromBody] RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Role _newRole = Mapper.Map<RoleViewModel, Role>(role);
            _roleRepository.Add(_newRole);
            await _roleRepository.Commit();
            var createdRole = Mapper.Map<Role, RoleViewModel>(_newRole);
            var json = JsonConvert.SerializeObject(createdRole, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpPost("UpdateRole", Name = "UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Role _newRole = Mapper.Map<RoleViewModel, Role>(role);
            _newRole.isActive = true;
            _roleRepository.Update(_newRole);
            await _roleRepository.Commit();
            var json = JsonConvert.SerializeObject(role, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpDelete("DeleteRole", Name ="DeleteRoleById")]
        public async Task<IActionResult> DeleteRole(int Id) 
        {
            Role _role = await _roleRepository.GetSingleAsync(Id);
            if(_role == null)
            {
                return new NotFoundResult();
            }
            _roleRepository.Delete(_role);
            await _roleRepository.Commit();
            return new NoContentResult();

        }
        [HttpGet("GetData",Name ="GetRoles")]
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
            q = (q == null) ? "" : q;
            int currentPage = page;
            int currentPageSize = pageSize;


            var data = await _roleRepository.FindByAsync(x => x.RoleName.Contains(q) || x.Description.Contains(q));
            var totalData = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalData / pageSize);
            Response.AddPagination(page, pageSize, totalData, totalPages);
            data.Skip(page * pageSize).Take(pageSize);
            var _result = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(data);
            var json = JsonConvert.SerializeObject(_result, _serializerSettings);
            return new OkObjectResult(json);
        }

    }
}
