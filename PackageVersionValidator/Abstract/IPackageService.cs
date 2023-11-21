namespace PackageVersionValidator.Abstract;

public interface IPackageService
{
    Task<Dictionary<string, string>> GetPackagesAsync(string servicePath, CancellationToken cancellationToken);
}