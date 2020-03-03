using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L06Hands_On.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L06Hands_On.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly CarsContext _context;

        public ValuesController(CarsContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public List<Car> Get()
        {
            List<Car> cars = _context.Cars.ToList();
            return  cars;
        }

        [HttpGet("{sortBy}/{num}")]
        public List<Car> Get(string sortBy, int num)
        {
            List<Car> cars = _context.Cars.ToList();
            List<Car> orderList = new List<Car>();
            if(sortBy == "NumOfPassengers") {
                for(int i = 0; i < cars.Count(); i++) {
                    if(cars[i].NumOfPassengers >= num) {
                        orderList.Add(cars[i]);
                    }
                }
            }
            if(sortBy == "Year") {
                cars.Sort(delegate(Car x, Car y)
                {
                    return y.Year.CompareTo(x.Year);
                });
                return cars;
            }
            
            return  orderList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Car Get(int id)
        {
            if (id == null)
            {
                return null;
            }

            var car = _context.Cars
                .FirstOrDefault(m => m.Id == id);
            if (car == null)
            {
                return null;
            }
            return car;
        }

        // POST api/values
        [HttpPost]
        public Car Post([FromBody] Car value)
        {
            if (ModelState.IsValid)
            {
                _context.Add(value);
                _context.SaveChanges();
                return _context.Cars.Find(value.Id);
            }
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Car Put(int id, [Bind("Id, Make, Model, Year, NumOfPassengers")] Car value)
        {
            if (id != value.Id)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(value);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(value.Id))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return value;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var car =  _context.Cars.Find(id);
            _context.Cars.Remove(car);
           _context.SaveChanges();
        }
    }
}
