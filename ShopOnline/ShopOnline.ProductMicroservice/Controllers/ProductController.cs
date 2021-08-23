using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.ProductMicroservice.Data;

namespace ShopOnline.ProductMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductDatabase _context;
        public ProductController()
        {
            _context = ProductDatabase.Initialize();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Entities.Product product)
        {
            var existingProduct = await _context.GetProductByDisplayName(product.DisplayName);

            if (existingProduct != null)
                return Conflict();

            await _context.AddProduct(product);

            await _context.SaveChanges();

            return Ok(product.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.GetAllProducts();

            if (products == null) 
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _context.GetProductById(id);

            if (product == null) 
                return NotFound();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _context.GetProductById(id);

            if (product == null) 
                return NotFound();

            _context.DeleteProduct(product);

            await _context.SaveChanges();

            return Ok(product.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Entities.Product productData)
        {
            var product = await _context.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                product.DisplayName = productData.DisplayName;
                product.Price = productData.Price;
                product.Tags = productData.Tags;

                await _context.SaveChanges();

                return Ok(product.Id);
            }
        }
    }
}