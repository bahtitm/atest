using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestAtest.Models;

namespace TestAtest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private OrderContext _context;
        

        public HomeController(ILogger<HomeController> logger, OrderContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        public IActionResult Index()
        {
            
                IEnumerable<Order> data;
                data = _context.Orders.ToList<Order>();
                return View(data);
            
                
           
            //    List<string> data = new List<string>();
            //    data.Add("1");
            //    data.Add("2");
            //    data.Add("3");
            //    data.Add("4");
            //    return View(data);

            

            
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
