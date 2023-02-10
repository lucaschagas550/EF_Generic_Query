using EF.Generic_Query.API.Data.Repositories.Pagination;
using EF.Generic_Query.API.Models;
using EF.Generic_Query.API.Services.Interfaces;
using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EF.Generic_Query.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices) =>
            _categoryServices = categoryServices;

        [HttpGet()]
        public async Task<ActionResult<Page<Category>>> Get([FromQuery] Pageable pagination) =>
            Ok(await _categoryServices.Get(pagination).ConfigureAwait(false));

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(Guid id) =>
            Ok(await _categoryServices.GetById(id).ConfigureAwait(false));

        [HttpPost()]
        public async Task<ActionResult<Category>> Post(Category category) =>
            Created($"{HttpContext.Request.PathBase}/{HttpContext.Request.Path}", await _categoryServices.Create(category).ConfigureAwait(false));

        [HttpPut()]
        public async Task<ActionResult<Category>> Put(Category category) =>
            Ok(await _categoryServices.Create(category).ConfigureAwait(false));

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(Guid id) =>
            Ok(await _categoryServices.Delete(id).ConfigureAwait(false));

        [HttpGet("pdf")]
        public async Task<IActionResult> ExportFile()
        {
            var renderer = new ChromePdfRenderer();

            var pdf = await renderer.RenderHtmlAsPdfAsync("<h1>Hello World</h1>").ConfigureAwait(false);
            pdf.SaveAs("output.pdf");
            
            return File(pdf.Stream.ToArray(), "application/pdf", "output.pdf");
        }
    }
}
