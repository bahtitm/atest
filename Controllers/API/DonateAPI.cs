using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TestAtest.HelperClass;
using TestAtest.Models;



namespace TestAtest.Controllers.API
{
    [Route("api/[controller]")]
    public class DonateAPI : Controller
    {
               
        Helper helper;
        private OrderContext _context;
        Order order;
        public DonateAPI(IConfiguration conf, OrderContext context)
        {
            helper = new Helper(conf);
            _context = context;
        }

        [HttpPost]
        public RedirectResult  Post(Donate donate)
        {
            order = new Order();
           if (ModelState.IsValid && donate != null)
            {

                order.Amount = donate.amountOfDonation;
                order.Descriptioin = donate.Comment;
                order.Number= Guid.NewGuid().ToString();
                order.Id = Guid.NewGuid().ToString();
                string resultOrderResponse = helper.RegisterOrder(order).Result; 

                dynamic parsejsonString = JObject.Parse(resultOrderResponse);
                
                
                string formUrl = parsejsonString.formUrl;
                int errorCode = parsejsonString.errorCode;
                 
                
                    if (errorCode == 0)
                    {
                        _context.Add(order);
                     _context.SaveChanges();
                        return Redirect(formUrl);
                    
                    }
                    else
                    {
                        return Redirect("/Home");
                    }
           }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                                return Redirect("/Home");
            }

        }
             
    }
}
