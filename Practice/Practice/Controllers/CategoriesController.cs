using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.DTOS.Requests;
using Practice.DTOS.Responses;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static readonly List<CategoryResponse> Lista = new()
        {
            new CategoryResponse {Id=1,Name="Tecno", Description="Categoria sobre tecnologia"},
            new CategoryResponse {Id=2,Name="Music", Description="Categoria sobre Musica" }


        };
        [HttpGet]
        public ActionResult<List<CategoryResponse>> GetAll()
        {
            return Ok(Lista);

        }
        [HttpGet("{id:int}")]
        public ActionResult<CategoryResponse> GetById(int id)
        {
            var Category = Lista.FirstOrDefault(x => x.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            return Ok(Category);

        }
        [HttpPost]
        public ActionResult<CategoryResponse> Crear([FromBody] CreateCategoryRequest request)
        {
            int NewId = Lista.Any() ? Lista.Max(x => x.Id) + 1 : 1;

            var Category = new CategoryResponse

            {

                Id = NewId,
                Name = request.Name,
                Description = request.Description,



            };
            Lista.Add(Category);
            return Ok(Category);

        }
        [HttpDelete("{id:int}")]
        public IActionResult EliminarCat(int id)
        {
            var Category = Lista.FirstOrDefault(c => c.Id == id);
            if (Category==null)
            {
                return NotFound();
            }
            Lista.Remove(Category);
            return NoContent();
        
        
        }

    }
}

