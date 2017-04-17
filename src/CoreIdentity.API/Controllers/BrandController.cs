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
    public class BrandController : Controller
    {
        private IBrandRepository _brandRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IProductAttributeRepository _attributeRepository;
        int page = 1;
        int pageSize = 10;
        public BrandController(IBrandRepository brandRepository, ICategoryRepository categoryRepository,
            IProductRepository productRepository, IProductAttributeRepository attributeRepository)
        {
            _brandRepository = brandRepository;
            _attributeRepository = attributeRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        [HttpGet("GetBrand", Name = "GetBrand")]
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
            var data = await _brandRepository.FindByAsync(x => x.BrandName.Contains(q) || x.Description.Contains(q));
            var totalData = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalData / pageSize);
            Response.AddPagination(page, pageSize, totalData, totalPages);
            data.Skip(page * pageSize).Take(pageSize);    
            var result = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(data);
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);

        }
        [HttpGet("GetAll", Name = "GetAllBrand")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _brandRepository.GetAll();
            var result = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(data);
            var json = JsonConvert.SerializeObject(result, _serializerSettings);
            return new OkObjectResult(json);
        }
        [HttpPost("AddBrand", Name = "AddBrand")]
        public async Task<IActionResult> Post([FromBody]BrandViewModel brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _newBrand = Mapper.Map<BrandViewModel, Brand>(brand);
            _brandRepository.Add(_newBrand);
            await _brandRepository.Commit();
            var _result = Mapper.Map<Brand, BrandViewModel>(_newBrand);
            var json = JsonConvert.SerializeObject(_result, _serializerSettings);
            return new OkObjectResult(json);
        }


    }
}
