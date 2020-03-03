using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApplication.models;
using Microsoft.AspNetCore.Mvc;

namespace BAckendApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly FruitContext _context; 

        public ValuesController(FruitContext context) {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<Fruit[]> Get()
        {
            return _context.Fruits.ToArray();
        }

        
        // POST api/values
        [HttpPost]
        public void Post([FromBody] Fruit fruit)
        {
            _context.Fruits.Add(fruit);
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Fruit value)
        {

            
        }

        

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Fruit fruit = _context.Fruits.Find(id);
            DeletedFruit deletedFruit = new DeletedFruit();
            deletedFruit.Name = fruit.Name;
            deletedFruit.Color = fruit.Color;
            _context.DeletedFruits.Add(deletedFruit);
            _context.Fruits.Remove(fruit);
            _context.SaveChanges();
        }
    }
}
