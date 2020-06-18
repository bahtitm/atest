
namespace TestAtest.Models
{
    public class DataForRequestOrder
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Amount { get; set; }

        public string OrderNumber { get; set; }

        public string FailURL { get; set; }
        public string ReturnURL { get; set; }
        public string Descriptioin { get; set; }
        public int Status { get; set; }

        public string OrderRegisterURL { get; set; }

        public string URLForGetStatus { get; set; }
    }
}
