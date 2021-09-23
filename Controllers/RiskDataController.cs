﻿using Microsoft.AspNetCore.Mvc;
using RiskAssessment.Models;
using RiskAssessment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RiskAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskDataController : ControllerBase
    {
        // GET: api/<RiskDataController>
        [HttpGet]

        public IEnumerable<CollateralRisk> Get()
        {
            using (var _context = new CustomerDbContext())
            {
                return _context.collateralRisks.ToList();

            }
        }

        // GET api/<RiskDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            using (var _context = new CustomerDbContext())
            {

                return "RiskPErcent" + _context.collateralRisks.FirstOrDefault(x=>x.LoanId == id).RiskPercent.ToString();
            }
        }

        // POST api/<RiskDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RiskDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RiskDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}