using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Custom;
using MentalMathApp.LevelConfigurations.Custom.Integer;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Services.Storage;

namespace MentalMathApp.Services.CustomManager;

public interface ICustomLevelManager
{
    MutableCustomConfiguration[] GetConfigurations(NumberTypes type);
    void UpdateMutableConfigurations();
    void ConfigurationPlayed(NumberConfigurationBase configuration);
    Task<NumberConfigurationBase> GetLastPlayedConfiguration();
}

public class CustomLevelManager : ICustomLevelManager
{
    private readonly IDeviceStorage _storage;
    private MutableCustomConfiguration[] _configurations;

    public CustomLevelManager(IDeviceStorage storage)
    {
        _storage = storage;
        _ = LoadConfigurationsAsync();
    }

    public MutableCustomConfiguration[] GetConfigurations(NumberTypes type)
    {
        return _configurations.Where(x => x.NumberType == type).ToArray();
    }

    public void UpdateMutableConfigurations()
    {
        // This is unintentionally bad. I thought passing data to view pages serializes them and deserializes them. A.k.a. passing by value
        // BUT turns out they are passed by reference.
        // This place holds and passes out mutable configurations. So if it gets changed anywhere, it gets changed here.
        // This is bad since now it's harder to know where they are modified. But you can just open mutable config class file and track by references I guess.
        // And this project is probably not going to be developed any further so whatever.

        _storage.SaveCustomConfigurations(_configurations);
    }

    public void ConfigurationPlayed(NumberConfigurationBase configuration)
    {
        if (!_configurations.Any(x => x.ConfigurationKey.Equals(configuration.ConfigurationKey)))
        {
            return;
        }

        _storage.SaveLastPlayedCustomConfiguration(configuration.ConfigurationKey);
    }

    public async Task<NumberConfigurationBase> GetLastPlayedConfiguration()
    {
        var configKey = await _storage.GetLastPlayedCustomConfiguration();
        if (string.IsNullOrEmpty(configKey))
        {
            return null;
        }

        var config = _configurations.FirstOrDefault(x => x.ConfigurationKey.Equals(configKey));

        return config;
    }

    private async Task LoadConfigurationsAsync()
    {
        var configurations = await _storage.GetCustomConfigurations();
        if (configurations is not null && configurations.Length != 0)
        {
            _configurations = configurations;
            return;
        }

        var initConfigs = GetInitialConfigurations();
        _configurations = initConfigs;

        await _storage.SaveCustomConfigurations(initConfigs);
    }

    private MutableCustomConfiguration[] GetInitialConfigurations()
    {
        return new MutableCustomConfiguration[]
        {
            new MutableCustomConfiguration(new CustomInteger10Configuration())
        };
    }
}
