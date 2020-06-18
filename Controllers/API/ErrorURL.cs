
using System.Linq;
using Microsoft.AspNetCore.Mvc;



namespace TestAtest.Controllers.API
{
    [Route("api/[controller]")]
    public class ErrorURL : Controller
    {
        OrderContext _context;
        public ErrorURL(OrderContext context)
        {
            _context=context;

        }
        
        [HttpGet("{id}")]
        public string Get(string id)
        {
                     
                var order = _context.Orders.FirstOrDefault(e => (e.Id.Contains(id)));
                order.Status = 1;
                _context.Update(order);
                _context.SaveChanges();
                return "не успешно ";
           
            
        }

        
    }
}
