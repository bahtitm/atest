
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TestAtest.HelperClass;




namespace TestAtest.Controllers.API
{
    [Route("api/[controller]")]
    public class SuccessURL : Controller
    {
        Helper helper;
        OrderContext _context;
        public SuccessURL(IConfiguration conf, OrderContext context)
        {

            helper = new Helper(conf);
            _context = context;

        }
       
        [HttpGet("{id}")]
        public string Get(string id, [FromQuery(Name = "orderId")] string orderId)
        {
            string status = helper.GetOrderStatus(orderId).Result;
            dynamic parsejsonString = JObject.Parse(status);

            if (parsejsonString.errorCode == 0 && parsejsonString.orderStatus == 2)
            {
                
                var order =_context.Orders.FirstOrDefault(e => (e.Id.Contains(id)));
                order.Status = 2;
                _context.Update(order);
                _context.SaveChanges();
                return  "все хорошо" + status ;
            }
            else
            {
                var order = _context.Orders.FirstOrDefault(e => (e.Id.Contains(id)));
                order.Status = -1;
                _context.Update(order);
                _context.SaveChanges();
                return "не все хорошо" + status;
            }
            
        }

       
    }
}
