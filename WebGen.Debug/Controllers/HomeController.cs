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
                var asm = Assembly.GetEntryAssembly(); // �� typeof(FavIcon).Assembly ����ȫ
                using Stream? stream = asm?.GetManifestResourceStream($"{asm.GetName().Name}.Assets.favicon.ico"); // ע������
                if (stream == null)
                    return File(Array.Empty<byte>(), "image/x-icon");

                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                return File(ms.ToArray(), contentType: "image/x-icon");
            }
            catch (FileNotFoundException ex)
            {
                // �� NoContent ��ʽ���ؿ��ļ�����Ȼ���� FileResult�����Կɼ��ݣ�
                return File(new byte[0], "application/octet-stream");
            }
        }
    }
}
