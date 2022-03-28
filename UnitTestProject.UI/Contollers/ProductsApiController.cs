#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnitTestProject.UI.Data;
using UnitTestProject.UI.Entities;

namespace UnitTestProject.UI.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IRepository<Product> _context;

        public ProductsApiController(IRepository<Product> context)
        {
            _context = context;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products= _context.GetAll();
            return Ok(products);
        }

        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.GetByid(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/ProductsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Update(product);


            if (!ProductExists(id))
            {
                return NotFound();
            }



            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            _context.Add(product);
          

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/ProductsApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.GetByid(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Delete(product);


            return NoContent();
        }

        private bool ProductExists(int id)
        {
            var product =  _context.GetOne(e => e.Id == id);
            if (product == null)
                return false;
            else
                return true;
           
        }
    }
}
