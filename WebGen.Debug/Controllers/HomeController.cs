using Microsoft.AspNetCore.Mvc;

namespace WebGen.Debug.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return WebGen.ASPNET.ContentsGen.Gen
                    (typeof(WebGen.Proj.App).Assembly, 
                    new("/Pages/Index.wxaml", 
                    UriKind.Relative));
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
