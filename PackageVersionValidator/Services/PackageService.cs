using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using PackageVersionValidator.Abstract;
using PackageVersionValidator.Settings;

namespace PackageVersionValidator.Services;

public class PackageService: IPackageService
{
    private readonly HttpClient _client;
    private readonly IOptions<PackageSettings> _options;

    public PackageService(HttpClient client, IOptions<PackageSettings> options)
    {
        _client = client;
        _options = options;
    }

    public async Task<Dictionary<string, string>> GetPackagesAsync(string servicePath, CancellationToken cancellationToken)
    {
        var path = servicePath + "/" + _options.Value.PackagesPath;
        var result = await _client.GetAsync(path, cancellationToken);
        var packages = await result.Content.ReadFromJsonAsync<Dictionary<string, string>>(cancellationToken: cancellationToken);
        
        return packages;
    }
}