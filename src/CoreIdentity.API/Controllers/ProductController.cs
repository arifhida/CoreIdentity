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
    public class ProductController : Controller
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IProductAttributeRepository _attributeRepository;
        int page = 1;
        int pageSize = 10;
        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository,
            IProductAttributeRepository attributeRepository)
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _attributeRepository = attributeRepository;
        }

        [HttpGet("GetProduct",Name ="GetProduct")]
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
            var data = await _productRepository.FindByAsync(x => x.SKU.Contains(q) ||
            x.ProductDescription.Contains(q) || x.ProductName.Contains(q) || x.Category.CategoryName.Contains(q));
            var totalData = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalData / pageSize);
            Response.AddPagination(page, pageSize, totalData, totalPages);
            data.Skip(page * pageSize).Take(pageSize);
            var _result = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(data);
            var json = JsonConvert.SerializeObject(_result, _serializerSettings);
            return new OkObjectResult(json);
        }

        public async Task<IActionResult> Add([FromBody]ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product _newProd = Mapper.Map<ProductViewModel, Product>(product);

            _productRepository.Add(_newProd);
            await _productRepository.Commit();
            var _result = Mapper.Map<Product, ProductViewModel>(_newProd);
            var json = JsonConvert.SerializeObject(_result, _serializerSettings);
            return new OkObjectResult(json);
        }



    }
}
