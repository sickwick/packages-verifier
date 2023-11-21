using Microsoft.Extensions.Options;
using PackageVersionValidator.Abstract;
using PackageVersionValidator.Settings;

namespace PackageVersionValidator.Services;

public class Worker : IHostedService
{
    private readonly ILogger<Worker> _logger;
    private readonly IPackageService _packageService;
    private readonly IOptions<PackageSettings> _packageSettings;

    public Worker(ILogger<Worker> logger, IPackageService packageService, IOptions<PackageSettings> packageSettings)
    {
        _logger = logger;
        _packageService = packageService;
        _packageSettings = packageSettings;
    }

    private async Task ValidatePackages(CancellationToken stoppingToken)
    {
        var services = _packageSettings.Value.Services;
        var requestedPackage = Environment.GetEnvironmentVariable(Constants.PACKAGE_NAME.ToString());
        var requestedVersion = Environment.GetEnvironmentVariable(Constants.PaCKAGE_VERSION.ToString());
        
        foreach (var service in services)
        {
            var packages = await _packageService.GetPackagesAsync(service, stoppingToken);
            if (!packages.ContainsKey(requestedPackage))
            {
                _logger.LogError("Service {service} does not contain package {package}", service, requestedPackage);
                return;
            }

            if (packages.TryGetValue(requestedPackage, out var version) && version != requestedVersion)
            {
                _logger.LogError("Service {service} does not contain package {package}  with version {version}", service, requestedPackage,requestedVersion);
            }
            
            _logger.LogInformation("Service {service} contains package {package} with version {version}", service, requestedPackage, requestedVersion);
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Package Version Validator is starting.");
        await ValidatePackages(cancellationToken);
        await StopAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Package Version Validator is stopping.");
        Environment.Exit(0);
        return Task.CompletedTask;
    }
}