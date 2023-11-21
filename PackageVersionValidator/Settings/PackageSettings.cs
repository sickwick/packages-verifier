namespace PackageVersionValidator.Settings;

public record PackageSettings
{
    public string[] Services { get; init; }
    public string PackagesPath { get; init; }
    public Uri BaseUrl { get; init; }
}