using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        [HttpGet("check/{product}")]
        public IActionResult CheckStock(string product)
        {
            // Simula inventario
            var stock = product.ToLower() switch
            {
                "laptop" => 10,
                "mouse" => 5,
                _ => 0
            };

            return Ok(new { Product = product, Available = stock });
        }
    }
}
