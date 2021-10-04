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
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RiskAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskDataController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RiskDataController));

        ///<summary>
        /// Search risks by GoldRate and LandRate
        /// </summary>
        /// <return>
        /// Returns list of risks likelt to occur
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/RiskData/{goldrate}/{landrate  }
        /// </remarks>
        /// <response code="200">Returns Risks after assessing</response>
        [HttpGet("{goldRate}/{landRate}")]
        public IActionResult Get(int goldRate, int landRate)
        {
            _log4net.Info("RiskData Get initiated!");
            using (var context = new CustomerDbContext())
            {
                //var list = context.collaterals.ToList();Where(a => Convert.ToInt64(a.NoOfUnits*a.unitValue) > Convert.ToInt64(a.NoOfUnits*goldRate))
                var list1 = context.collateral.ToList();
                var ls = new List<Collateral>();
                foreach (var item in list1)
                {
                    if (item.Type == "Gold" && item.unitValue > goldRate)
                    {
                        _log4net.Info("Item that possess risk for gold added to list");
                        ls.Add(item);
                    }
                    if (item.Type == "Estate" && item.unitValue > landRate)
                    {
                        _log4net.Info("Item that possess risk for estate add to list");
                        ls.Add(item);
                    }
                }
                _log4net.Info("List of probable risks returned");
                return Ok(ls);
            }
        }

    // POST api/<RiskDataController>
        [HttpPost]
        public IActionResult Post([FromBody] CurrentValue cv)
        {
            _log4net.Info("Post Method Called!");
            using (var context = new CustomerDbContext())
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
        [HttpGet]
        public IActionResult Get()
        {
            _log4net.Info("Get MEthod called which reads a flat file and returns risks");
            var random = new Random();
            var lines = System.IO.File.ReadAllLines("Controllers\\TextFile.Txt").ToList();
            var index = random.Next(lines.Count);
            var g = Convert.ToInt32(lines[index]);
            using (var context = new CustomerDbContext())
            {
                var list1 = context.collateral.ToList();
                var ls = new List<Collateral>();
                foreach (var item in list1)
                {
                    if (item.Type == "Gold" && item.unitValue > g)
                    {
                        ls.Add(item);
                    }
                }
                return Ok(ls);
            }
        }
    }
}




















/*
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
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RiskAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskDataController : ControllerBase
    {
        ///<summary>
        /// Search risks by GoldRate and LandRate
        /// </summary>
        /// <return>
        /// Returns list of risks likelt to occur
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/RiskData/{goldrate}/{landrate  }
        /// </remarks>
        /// <response code="200">Returns Risks after assessing</response>
        [HttpGet("{goldRate}/{landRate}/{id}")]
        public async Task<IActionResult> Get(int goldRate, int landRate, int id)
        {
            var CustomerLoan = new Collateral();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:25168/api/Collateral/");
                HttpResponseMessage GetJob = await client.GetAsync($"{id}");
                if (GetJob.IsSuccessStatusCode)
                {
                    var result = GetJob.Content.ReadAsStringAsync().Result;
                    CustomerLoan = JsonConvert.DeserializeObject<Collateral>(result);
                }
                else
                {
                    return StatusCode(500,"Error occured");
                }
            using (var context = new CustomerDbContext())
                {
                    try
                    {
                        if (CustomerLoan.Type == "Gold" && CustomerLoan.unitValue > goldRate)
                        {
                            return Ok(CustomerLoan);
                            // ls.Add(item);
                        }
                        else
                        {
                            return Ok("No Risks Found");
                        }
                    }
                    catch(Exception)
                    {
                        return  StatusCode(500,"No data found");
                    }
/*              var list1 = context.collateral.ToList();
                var ls = new List<Collateral>();
                foreach (var item in list1)
                {
                    if (item.Type == "Gold" && item.unitValue > goldRate)
                    {
                        ls.Add(item);
                    }
                    if (item.Type == "Estate" && item.unitValue > landRate)
                    {
                        ls.Add(item);
                    }
                }*/
                //return Ok(ls);
/*            }
        }
        }
        [HttpGet("{id}")]
public async Task<IActionResult> GetAsync(int id)
{
    using (var client = new HttpClient())
    {
        var CustomerLoan = new Collateral();
        client.BaseAddress = new Uri("http://localhost:25168/api/Collateral/");
        HttpResponseMessage GetJob = await client.GetAsync($"{id}");
        if (GetJob.IsSuccessStatusCode)
        {
            var result = GetJob.Content.ReadAsStringAsync().Result;
            CustomerLoan = JsonConvert.DeserializeObject<Collateral>(result);
            return Ok(CustomerLoan.NoOfUnits);
            //return RedirectToAction("Index");

        }
        else
        {
            return StatusCode(500, "An error occured");
            //ViewBag.Message = String.Format("No Data Found");
            //return View();
        }
    }
}
    }
}
*/