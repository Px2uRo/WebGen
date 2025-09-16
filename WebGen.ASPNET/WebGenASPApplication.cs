using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
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
    /// <summary>
    /// 自定义动态路由数据源
    /// </summary>
    public class DynamicEndpointDataSource : EndpointDataSource
    {
        private readonly ConcurrentDictionary<string, RequestDelegate> _routes = new();
        private readonly List<Endpoint> _endpoints = new();
        private CancellationTokenSource _cts = new();

        public override IReadOnlyList<Endpoint> Endpoints => _endpoints;

        public override IChangeToken GetChangeToken() => new CancellationChangeToken(_cts.Token);

        public void AddRoute(string pattern, RequestDelegate handler)
        {
            _routes[pattern] = handler;
            _rebuildEndpoints();
        }

        public void RemoveRoute(string pattern)
        {
            _routes.TryRemove(pattern, out _);
            _rebuildEndpoints();
        }

        private void _rebuildEndpoints()
        {
            var newEndpoints = new List<Endpoint>();

            foreach (var kv in _routes)
            {
                var builder = new RouteEndpointBuilder(
                    kv.Value,
                    RoutePatternFactory.Parse(kv.Key),
                    order: 0);

                newEndpoints.Add(builder.Build());
            }

            _endpoints.Clear();
            _endpoints.AddRange(newEndpoints);

            var oldCts = _cts;
            _cts = new CancellationTokenSource();
            oldCts.Cancel();
        }

    }

    public static class BuilderUtil
    {
        public static TApp Build<TApp>(this WebApplicationBuilder web) where TApp : WebGenASPApplication, new()
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

        DynamicEndpointDataSource _dynamicSource = new DynamicEndpointDataSource();
        public DynamicEndpointDataSource DynamicSource => _dynamicSource;
        public static new WebGenASPApplication Current { get; private set; }
        public WebApplication? Inner { get; set; }
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
        public virtual void Run()
        {
            Inner?.UseRouting();
            Inner?.UseEndpoints(endpoints =>
            {
                endpoints.DataSources.Add(_dynamicSource);
            });
            Inner?.Run();
        }
    }
}
