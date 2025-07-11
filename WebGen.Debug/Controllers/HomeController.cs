using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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


    [ApiController]
    [Route("favicon.ico")]
    public class FavIcon : ControllerBase
    {
        [HttpGet]
        public FileResult Get()
        {
            try
            {
                var asm = Assembly.GetEntryAssembly(); // 或 typeof(FavIcon).Assembly 更安全
                using Stream? stream = asm?.GetManifestResourceStream($"{asm.GetName().Name}.Assets.favicon.ico"); // 注意命名
                if (stream == null)
                    return File(Array.Empty<byte>(), "image/x-icon");

                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                return File(ms.ToArray(), contentType: "image/x-icon");
            }
            catch (FileNotFoundException ex)
            {
                // 用 NoContent 形式返回空文件（虽然返回 FileResult，但仍可兼容）
                return File(new byte[0], "application/octet-stream");
            }
        }
    }
}
