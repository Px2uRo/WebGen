using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using WebGen.Converters;

namespace WebGen.ASPNET
{
    public static class ContentsGen
    {
        private static IPageConverter _appCv = new ASPNETConvertor();
        /// <summary>
        /// 从 wxaml 文件生成内容结果，会判断文件是否存在，若不存在则返回 404。
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static ContentResult Gen(Assembly asm, Uri uri,HttpRequest request = null)
        {
            if (request != null)
            {
                if(_appCv is ASPNETConvertor asp)
                {
                    asp.SetRequest(request);
                }
            }
            string path = uri.OriginalString.TrimStart('/')
                .Replace('/', '.')
                .Replace('\\', '.');

            string defaultNamespace = asm.GetName().Name;
            string resourceName = $"{defaultNamespace}.{path}";

            using Stream? wxamlstream = asm.GetManifestResourceStream(resourceName);
            if (wxamlstream == null)
                throw new FileNotFoundException($"找不到资源：{resourceName}");

            using StreamReader reader = new StreamReader(wxamlstream);

            // 2. 试图读取 .wxaml.cs 内容（如果存在）
            string csResourceName = $"{resourceName}.cs";
            string? codeBehindText = null;

            using Stream? csStream = asm.GetManifestResourceStream(csResourceName);
            if (csStream != null)
            {
                using StreamReader csReader = new StreamReader(csStream);
                codeBehindText = csReader.ReadToEnd();
            }

            return new ContentResult
            {
                Content = _appCv.Convert(reader.ReadToEnd(),codeBehindText),
                ContentType = "text/html"
            };
        }
    }
}
