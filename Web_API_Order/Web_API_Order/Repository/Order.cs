using System.Collections.Generic;
using System.Threading.Tasks;
using Web_API_Order.Models;



namespace Web_API_Order.Repository
{
    public class Order : IOrder
    {
        public async Task<IEnumerable<Orders>> GetOrder()
        {
            var orders = new List<Orders>
            {
                new Orders
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    Items = new List<Items> { new Items { ProductID = 1, ProductName = "Product A" } },
                    TotalAmount = 20.0f,
                    Status = "Pending"
                },
                new Orders
                {
                    Id = 2,
                    OrderDate = DateTime.UtcNow,
                    Items = new List<Items> { new Items { ProductID = 2, ProductName = "Product B" } },
                    TotalAmount = 20.0f,
                    Status = "Pending"
                },
                new Orders
                {
                    Id = 3,
                    OrderDate = DateTime.UtcNow,
                    Items = new List<Items> { new Items { ProductID = 3, ProductName = "Product C" } },
                    TotalAmount = 25.0f,
                    Status = "Complete"
                }
            };

            return await Task.FromResult(orders);
        }
    }
}
