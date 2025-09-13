using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebGen.Controls;
using Wedency;

namespace WebGen.ASPNET
{
    public static class BuilderUtil
    {
        public static TApp Build<TApp>(this WebApplicationBuilder web) where TApp : WebGenASPApplication, new ()
        {
            var app = web.Build();
             var res = new TApp();
            res.Inner = app;
            return res;
        }

    }
    
    public static class CodeGenExtensions
    {
        public static WebGenASPApplication UseASPNET(this WebGenASPApplication app)
        {
            app.Inner.MapGet("CodeGenInfo.json", () => Results.Content($"{{\"useaspnet\":true}}",contentType: "application/json"));
        return app;
        }
    }
    public class WebGenASPApplication : WebGenApplication
    {
        public static new WebGenASPApplication Current { get; private set; }
        public WebApplication Inner { get; set; }
        public WebGenASPApplication()
        {
        }
        public static WebGenASPApplication Create(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            var inner = builder.Build();

            var res = new WebGenASPApplication();
            Current = res;
            res.Inner = inner;
            return res;
        }
        
        public static WebApplicationBuilder GetBuilder<TApp>(string[] args) where TApp : WebGenASPApplication, new()
        {
            var builder = WebApplication.CreateSlimBuilder(args);
            return builder;
        }

        public static TApp Create<TApp>(string[] args) where TApp : WebGenASPApplication, new()
        {
            var builder = GetBuilder<TApp>(args);
            return Create<TApp>( builder,args); 
        }

        public static TApp Create<TApp>(WebApplicationBuilder builder,string[] args) where TApp : WebGenASPApplication, new()
        {
            var inner = builder.Build();

            var res = new TApp();
            res.Inner = inner;
            return res;
        }
        public virtual void Run() => Inner.Run();
    }
}
