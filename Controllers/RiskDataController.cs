using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiskAssessment.Models;
using RiskAssessment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RiskAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskDataController : ControllerBase
    {
        // GET: api/<RiskDataController>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            int goldunit = 10;

            using (var context = new CustomerDbContext())
            {
                //var list = context.collaterals.ToList();
                var list1 = context.collateral.Where(a => (a.NoOfUnits) >= goldunit).ToList();
                return Ok(list1);
            }
            /*using (var _context = new CustomerDbContext())
            {
                try
                {
                    var risks = _context.collateralRisks.ToList();
                    if (risks.Count == 0)
                    {
                        return StatusCode(404, "No Risk found");
                    }
                    return Ok(risks);
                }
                catch (Exception)
                {
                    return StatusCode(500, "An error has occured");

                }
                //return Ok();
                //return _context.collateralRisks.ToList();

            }*/
        }
        /*[HttpGet]
        public IEnumerable<Collateral> GetCollaterals()
        {
            int goldunit = 10;
            
            using (var context = new CustomerDbContext())
            {
               //var list = context.collaterals.ToList();
                var list1 = context.collaterals.Where(a => (a.NoOfUnits * a.unitValue) > goldunit).ToList();
                return list1;
            }
        }*/

        // GET api/<RiskDataController>/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            using (var _context = new CustomerDbContext())
            {
               // var DbRows = _context.
              
             
                return "RiskPercent : " + _context.collateralRisks.FirstOrDefault(x=>x.LoanId == id).RiskPercent.ToString();
            }
        }

        [HttpGet("{goldRate}/{landRate}")]
        [Authorize]
        public IActionResult Get(int goldRate, int landRate)
        {
            using (var context = new CustomerDbContext())
            {
                //var list = context.collaterals.ToList();Where(a => Convert.ToInt64(a.NoOfUnits*a.unitValue) > Convert.ToInt64(a.NoOfUnits*goldRate))
                var list1 = context.collateral.ToList();
                var ls =new List<Collateral>();
                foreach (var item in list1)
                {
                    if(item.Type=="Gold" && item.unitValue > goldRate)
                    {
                        ls.Add(item);
                    }
                    if (item.Type == "Estate" && item.unitValue > landRate)
                    {
                        ls.Add(item);
                    }
                }
                
                return Ok(ls);
            }
        }

            // POST api/<RiskDataController>
            [HttpPost]
        public IActionResult Post([FromBody] CurrentValue cv)
        {
            using(var context = new CustomerDbContext())
            {
                try
                {
                    context.currentValues.Add(cv);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return StatusCode(500, "An Error Occured!");
                }
                return Ok(context.currentValues.ToList());
            }
        }

        // PUT api/<RiskDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RiskDataController>/5
        [HttpDelete]
        public void Delete()
        {
        }
    }
}
