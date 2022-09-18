using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Custom;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Services.CustomManager.Models;
using Newtonsoft.Json;

namespace MentalMathApp.Services.Storage;

public interface IDeviceStorage
{
	Task SaveBeatenIntegerLevelNames(List<string> names);
	Task SaveBeatenRationalLevelNames(List<string> names);
	Task<List<string>> GetBeatenLevelNames(NumberTypes type);
	void ClearStoryProgress();
	Task<MutableCustomConfiguration[]> GetCustomConfigurations();
	Task SaveCustomConfigurations(MutableCustomConfiguration[] configs);
	Task<List<HistoryModel>> GetHistoryModels(string levelKey);
	Task SaveHistoryModels(string levelKey, List<HistoryModel> models);
	void Clear(string key);
}

public class DeviceStorage : IDeviceStorage
{
	private readonly ISecureStorage _storage;

	public DeviceStorage(ISecureStorage storage)
	{
		_storage = storage;
	}

	public async Task<List<string>> GetBeatenLevelNames(NumberTypes type)
	{
		var key = type switch
		{
			NumberTypes.Integer => Keys.Story.BeatenIntegerLevels,
			NumberTypes.Rational => Keys.Story.BeatenRationalLevels,
			_ => throw new NotImplementedException($"{nameof(GetBeatenLevelNames)} - type not implemented: {type}")
		};

		var value = await _storage.GetAsync(key);

		if (value is null)
		{
			return new();
		}

		return JsonConvert.DeserializeObject<List<string>>(value);
	}

	public async Task SaveBeatenIntegerLevelNames(List<string> names)
	{
		var value = JsonConvert.SerializeObject(names);

		await _storage.SetAsync(Keys.Story.BeatenIntegerLevels, value);
	}

	public async Task SaveBeatenRationalLevelNames(List<string> names)
	{
		var value = JsonConvert.SerializeObject(names);

		await _storage.SetAsync(Keys.Story.BeatenRationalLevels, value);
	}

	public async Task<MutableCustomConfiguration[]> GetCustomConfigurations()
	{
		var result = await _storage.GetAsync(Keys.Custom.CustomConfigurations);

		if (result is null)
		{
			return null;
		}

		var configurations = JsonConvert.DeserializeObject<MutableCustomConfiguration[]>(result);

		return configurations;
	}

	public async Task SaveCustomConfigurations(MutableCustomConfiguration[] configs)
	{
		if (configs is null || configs.Length == 0)
		{
			return;
		}

		var result = JsonConvert.SerializeObject(configs);

		await _storage.SetAsync(Keys.Custom.CustomConfigurations, result);
	}

	public async Task<List<HistoryModel>> GetHistoryModels(string levelKey)
	{
		if (string.IsNullOrEmpty(levelKey))
		{
			return null;
		}

		var result = await _storage.GetAsync(levelKey);

		if (result is null)
		{
			return new List<HistoryModel>();
		}

		var historyModels = JsonConvert.DeserializeObject<List<HistoryModel>>(result);

		return historyModels.OrderByDescending(x => x.Date).ToList();
	}

	public async Task SaveHistoryModels(string levelKey, List<HistoryModel> models)
	{
		if (models is null || !models.Any())
		{
			return;
		}

		var result = JsonConvert.SerializeObject(models);

		await _storage.SetAsync(levelKey, result);
	}

	public void Clear(string key) => _storage.Remove(key);

	public void ClearStoryProgress()
	{
		_storage.Remove(Keys.Story.BeatenIntegerLevels);
		_storage.Remove(Keys.Story.BeatenRationalLevels);
		_storage.Remove(Keys.Custom.CustomConfigurations);
	}
}
