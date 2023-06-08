using APIGateway;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings."+ environment + ".json", optional: false)
       .Build();

WebProtocolSettings settings_Web = new WebProtocolSettings();
config.GetSection("WebProtocolSettings").Bind(settings_Web);


IWebHostBuilder builder = new WebHostBuilder();
builder.ConfigureServices(s =>
{
    s.AddSingleton(builder);
});
builder.UseKestrel()
       .UseContentRoot(Directory.GetCurrentDirectory())
       .UseStartup<Startup>()
.UseUrls(settings_Web.Url+":"+ settings_Web.Port);


var host = builder.Build();
host.Run();

public class WebProtocolSettings
{
    public string Url { get; set; }
    public int Port { get; set; }
}