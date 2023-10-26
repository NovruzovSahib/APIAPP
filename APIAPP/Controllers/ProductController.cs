using APIAPP.Data;
using APIAPP.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var list = _dbContext.Products.AsEnumerable();
            return list;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Product product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            return new JsonResult(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product model)
        {
            if (model is null)
            {
                return BadRequest();
            }
            await _dbContext.Products.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Post), new { id = model.Id }, model);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Product model)
        {
            Product product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            product.Name = model.Name;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Product product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
