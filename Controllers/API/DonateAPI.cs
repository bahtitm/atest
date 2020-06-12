using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TestAtest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAtest.Controllers.API
{
    [Route("api/[controller]")]
    public class DonateAPI : Controller
    {
        // GET: api/<controller>
        static readonly Dictionary<Guid, Donate> donates = new Dictionary<Guid, Donate>();

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Donate donate)
        {


            if (ModelState.IsValid && donate != null)
            {
                // Convert any HTML markup in the status text.
                donate.amountOfDonationDonate = HttpUtility.HtmlEncode(donate.amountOfDonationDonate);

                // Assign a new ID.
                var id = Guid.NewGuid();
                donates[id] = donate;

                // Create a 201 response.
                var response = new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent(donate.amountOfDonationDonate)
                };
                response.Headers.Location =
                    new Uri(Url.Link("DefaultApi", new { action = "status", id = id }));
                return response;
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }





        }
        [HttpGet]
        public Donate Status(Guid id)
        {
            Donate donate;
            if (donates.TryGetValue(id, out donate))
            {
                return donate;
            }
            else
            {
                throw new HttpRequestException("exep");
            }
        }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
