using System.Collections.Generic;
using System.Threading.Tasks;
using Web_API_Order.Models; // or the namespace where Order lives

namespace Web_API_Order.Repository
{   
    public interface IOrder
    {
        Task<IEnumerable<Orders>> GetOrder();
    }
}
    