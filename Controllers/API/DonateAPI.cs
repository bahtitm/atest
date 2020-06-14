using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public HttpResponseMessage Post(Donate donate)
        {
           if (ModelState.IsValid && donate != null)
            {
                RegisterOrder(donate);
                var id = Guid.NewGuid();
                donates[id] = donate;

                var response = new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent(donate.Comment)
                };
                Response.Redirect("https://www.google.com");
                return response;
                
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }

        }
        private int GetSumASCIIcodFrom(string value)
        {
            Encoding ascii = Encoding.ASCII;

            Byte[] encodedBytes = ascii.GetBytes(value);
            
            int sumASCIIcode = 0;
            foreach (Byte b in encodedBytes)
            {
                sumASCIIcode += b;
                
            }
            return sumASCIIcode;
        }
        public async void RegisterOrder(Donate donate)
        {
            Order order = new Order();
            order.UserName= "client10";

            order.Password= GetSumASCIIcodFrom("client10-spasem-mir").ToString();
            order.Amount = donate.amountOfDonation;
            order.OrderNumber = Guid.NewGuid().ToString();
            order.ReturnURL = "/api/SuccessURL";

            order.FailURL = "/api/ErrorURL";
            order.Descriptioin = donate.Comment;
                        
            
            
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://attest.turkmen-tranzit.com/payment/rest/register.do");
            var json = JsonConvert.SerializeObject(order);

            
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(client.BaseAddress,data);
           string  result = response.Content.ReadAsStringAsync().Result;
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            foreach (var keyvalue in jsonData)
            {

                Console.WriteLine(keyvalue.Value + keyvalue.Value);  // this will only display the value of that
                                               // attribute / key 

            }
            //Debug.WriteLine(result+"tut ya toje");
            
        }
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
