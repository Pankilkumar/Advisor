using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using advisor_backend.Models;
using System;

namespace advisor_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvisorsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
         private readonly Random _random;

        public AdvisorsController(IMemoryCache cache)
        {
            _cache = cache;
            _random = new Random();
        }

        // GET api/advisors
        //   Localhost:5074/api/advisors
        [HttpGet]
        public IActionResult GetAdvisors()
        {
            var advisors = _cache.Get<List<Advisor>>("Advisors");
            if (advisors == null)
            {
                advisors = new List<Advisor>();
                _cache.Set("Advisors", advisors);
            }
            return Ok(advisors);
        }
        //to look particular user details itself
        //  Localhost:5074/api/advisors
        [HttpGet("{id}")]
        public IActionResult GetAdvisor(int id)
        {
            var advisors = _cache.Get<List<Advisor>>("Advisors");
            var advisor = advisors?.FirstOrDefault(a => a.Id == id);
            if (advisor == null)
            {
                return NotFound();
            }
            return Ok(advisor);
        }

        //this action will create new Advisor according the requirment.
        [HttpPost]
        public IActionResult CreateAdvisor(Advisor advisor)
        {
            var advisors = _cache.Get<List<Advisor>>("Advisors");
            if (advisors == null)
            {
                advisors = new List<Advisor>();
                _cache.Set("Advisors", advisors);
            }
              int randomNumber = _random.Next(1, 101);
            if (randomNumber <= 60)
            {
                advisor.HealthStatus = "Green";
            }
            else if (randomNumber <= 80)
            {
                advisor.HealthStatus = "Yellow";
            }
            else
            {
                advisor.HealthStatus = "Red";
            }

            advisor.Id = advisors.Count + 1;
            advisors.Add(advisor);
            _cache.Set("Advisors", advisors);
            return Ok(advisor);
        }
         

        // PUT   Localhost:5074/api/advisors

        [HttpPut("{id}")]
        public IActionResult UpdateAdvisor(Int32 id, Advisor updatedAdvisor)
        {
            var advisors = _cache.Get<List<Advisor>>("Advisors");
            var advisor = advisors?.FirstOrDefault(a => a.Id == id);
            if (advisor == null || advisors.Count == 0)
            {
               return NotFound();
            }
            advisor.Name = updatedAdvisor.Name;
            advisor.SIN = updatedAdvisor.SIN;
            advisor.Address = updatedAdvisor.Address;
            advisor.Phone = updatedAdvisor.Phone;
            advisor.HealthStatus = updatedAdvisor.HealthStatus;
            _cache.Set("Advisors", advisors);
            return Ok(advisor);
        }

        // DELETE api/advisors/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAdvisor(int id)
        {
            var advisors = _cache.Get<List<Advisor>>("Advisors");
            var advisor = advisors?.FirstOrDefault(a => a.Id == id);
            if (advisor == null)
            {
                return NotFound();
            }
            advisors.Remove(advisor);
            _cache.Set("Advisors", advisors);
            return Ok();
        }
    }
}