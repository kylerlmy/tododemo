using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace AspNetCore.ToDo.IntergrationTests {
    public class TestFixture : IDisposable {
        private readonly TestServer _server;
        public void Dispose () {
            Client.Dispose();
            _server.Dispose();
        }

        public TestFixture () {
            var builder = new WebHostBuilder ()
            .UseStartup<AspNetCore.ToDo.Startup>()
            .ConfigureAppConfiguration((context,configBuilder)=>{
                configBuilder.SetBasePath(Path.Combine(
                    Directory.GetCurrentDirectory(),"..\\..\\..\\..\\AspNetCore.ToDo"
                ));
                configBuilder.AddJsonFile("appsettings.json");

                //为Facebook 中间件添加假的配置信息（以免启动时报错）
                configBuilder.AddInMemoryCollection(new Dictionary<string,string>()
                {
                    ["Facebook:AppId"]="fake-app-id",
                    ["Facebook:AppSecret"]="fake-app-secret"
                });
            });
            _server=new TestServer(builder);

            Client=_server.CreateClient();
            Client.BaseAddress=new Uri("http://localhost:5000");

        }

    public HttpClient Client { get; set; }
    }
}