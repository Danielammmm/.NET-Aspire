using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace OrdersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new();

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order, [FromServices] IHttpClientFactory httpClientFactory)
        {
            var client = httpClientFactory.CreateClient("inventory");

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync($"/inventory/check/{order.Product}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al contactar inventario: {ex.Message}");
            }

            if (!response.IsSuccessStatusCode)
                return BadRequest("Inventario no accesible");

            var json = await response.Content.ReadAsStringAsync();

            InventoryResponse? stock;
            try
            {
                stock = JsonSerializer.Deserialize<InventoryResponse>(json);
            }
            catch
            {
                return BadRequest("Respuesta de inventario no válida");
            }
            Console.WriteLine($"Producto: {stock?.product}, Disponible: {stock?.available}, Solicitado: {order.Quantity}");


            if (stock is null || stock.available < order.Quantity)
                return BadRequest("Stock insuficiente");

            order.Id = Guid.NewGuid();
            _orders.Add(order);

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(Guid id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return order != null ? Ok(order) : NotFound();
        }
    }

    public class Order
    {
        public Guid Id { get; set; }
        public string Product { get; set; } = "";
        public int Quantity { get; set; }
    }

    public class InventoryResponse
    {
        public string product { get; set; } = "";  
        public int available { get; set; }         
    }

}
