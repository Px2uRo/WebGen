using System.Text.Json.Serialization;
using WebGen.ASPNET;

namespace WebGen.MinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if true
            var builder = WebGenASPApplication.GetBuilder<App>(args);
            var app = builder.Build<App>(); 
            var todosApi = app.Inner.MapGroup("/todos");
            var index = app.Inner.MapGroup("index");
            index.MapGet("/", (_) => { return new(() => { Results.Ok(); }); });
            app.UseASPNET().Run();  
#endif
        }
    }

    public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

    [JsonSerializable(typeof(Todo[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
