using Microsoft.Extensions.Options;
using PackageVersionValidator;
using PackageVersionValidator.Abstract;
using PackageVersionValidator.Services;
using PackageVersionValidator.Settings;

var requestedPackage = args.First();
var requestedVersion = args.Skip(1).First();

Environment.SetEnvironmentVariable(Constants.PACKAGE_NAME.ToString(), requestedPackage);
Environment.SetEnvironmentVariable(Constants.PaCKAGE_VERSION.ToString(), requestedVersion);

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<PackageSettings>(context.Configuration.GetSection("PackageSettings"));
        services.AddHostedService<Worker>();
        // services.AddSingleton<IPackageService, PackageService>();
        services.AddHttpClient<IPackageService, PackageService>((provider, client) =>
        {
            var uri = provider.GetRequiredService<IOptions<PackageSettings>>();
            client.BaseAddress = uri.Value.BaseUrl;
        });
    })
    .Build();

await host.RunAsync();