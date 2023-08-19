using Microsoft.AspNetCore.Mvc;

namespace AnimalShelterAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : ControllerBase
    {
        [HttpGet("docs")]
        public ContentResult GetDocumentation()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Docs", "Documentation.html");
            string htmlContent = System.IO.File.ReadAllText(filePath);

            return Content(htmlContent, "text/html");
        }
    }
}
