using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestAtest.Models;

namespace TestAtest.HelperClass
{
    public  class Helper
    {
        public   IConfiguration Configuration;
       public  Helper(IConfiguration conf)
        {
            Configuration = conf;
        }
        
        private   int GetSumASCIIcodFrom(string value)
        {
            Byte[] encodedBytes = Encoding.ASCII.GetBytes(value);
            int sumASCIIcode = 0;
            foreach (Byte b in encodedBytes)
            {
                sumASCIIcode += b;
            }
            return sumASCIIcode;
        }
        public   async Task<string> RegisterOrder(Order order)
        {
            string Salt = Configuration["Shop:Salt"];
            DataForRequestOrder orderRequest = new DataForRequestOrder();
            orderRequest.UserName = Configuration["Shop:ClientName"]; 
            orderRequest.Password = GetSumASCIIcodFrom(orderRequest.UserName+ Salt).ToString();
            orderRequest.Amount = order.Amount;
            orderRequest.OrderNumber = order.Number;//Guid.NewGuid().ToString();
            orderRequest.ReturnURL = Configuration["ReturnURL"]+order.Id;
            orderRequest.OrderRegisterURL = Configuration["OrderRegisterURL"];
            orderRequest.FailURL = Configuration["FailURL"]  + order.Id;
            orderRequest.Descriptioin = order.Descriptioin;

            var client = new HttpClient();
            client.BaseAddress = new Uri(orderRequest.OrderRegisterURL);
            var json = JsonConvert.SerializeObject(orderRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress, data);
            string result = response.Content.ReadAsStringAsync().Result;
            return result;


        }
        public  async Task<string> GetOrderStatus(string orderId)
        {
            string Salt = Configuration["Shop:Salt"];
            DataForRequestOrder orderRequest = new DataForRequestOrder();
            orderRequest.UserName = Configuration["Shop:ClientName"];
            orderRequest.Password = GetSumASCIIcodFrom(orderRequest.UserName + Salt).ToString();
            orderRequest.OrderId = orderId;
            orderRequest.URLForGetStatus = Configuration["OrderStatusURL"];

            var client = new HttpClient();
            client.BaseAddress = new Uri(orderRequest.URLForGetStatus);
            var json = JsonConvert.SerializeObject(orderRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress, data);
            string result = response.Content.ReadAsStringAsync().Result;
          
            return result;
        }
    }
}
