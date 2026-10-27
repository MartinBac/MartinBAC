using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.DTOS.Requests;
using Practice.DTOS.Responses;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<ProductResponse> lista = new()
        {
            new ProductResponse {Id=1,Name="Teclado",Price= 50},
            new ProductResponse {Id=2,Name="Mouse", Price=20}
        };
        [HttpGet]
        public ActionResult <List<ProductResponse>> ObtenerTodos()
        {
            return Ok(lista);
        }
        [HttpGet("{id:int}")]
        public ActionResult <List<ProductResponse>> ObtenerPorId(int id)
        {
        var prod = lista.FirstOrDefault(p =>p.Id == id);
            if (prod == null)
            {
                return NotFound();

            }
            return Ok(prod);
        }
        [HttpPost]
        public ActionResult <ProductResponse> Crear([FromBody] CreateProductRequest request)
        {
            int IdNew = lista.Any() ? lista.Max(p => p.Id) + 1 : 1;
            var nuevoProd = new ProductResponse { Id = IdNew, Name= request.Name, Price = request.Price };
            lista.Add(nuevoProd);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoProd.Id }, nuevoProd);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var prod= lista.FirstOrDefault(p =>p.Id == id);
            if (prod==null)
            {
                return NotFound();
            }
            lista.Remove(prod);
            return NoContent();    

        }




    }
}
