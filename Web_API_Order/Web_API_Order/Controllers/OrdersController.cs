using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API_Order.Models;
using Web_API_Order.Repository;

namespace Web_API_Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _orderRepository;
        public OrdersController(IOrder order)
        {
            _orderRepository = order;
        }

        // Require any authenticated user (token must be valid)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            //// Middleware already returns 401 if token missing/invalid, but you can check:
            //if (!(User.Identity?.IsAuthenticated ?? false))
            //    return Unauthorized();

            // Get common claims:
            var username = User.Identity?.Name ?? User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst("role")?.Value; // or ClaimTypes.Role

            // Use repository to get orders (example)
            var orders = await _orderRepository.GetOrder();

            // Optionally filter or customize response based on user/role
            return Ok(new { requestedBy = username, role, data = orders });
        }

        // Example of role-based authorization: only users in "Admin" role can call this
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<IActionResult> GetOrdersForAdmin()
        {
            var orders = await _orderRepository.GetOrder();
            return Ok(orders);
        }
    }
}