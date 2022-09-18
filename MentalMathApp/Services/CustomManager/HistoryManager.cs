using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Services.CustomManager.Models;
using MentalMathApp.Services.Storage;

namespace MentalMathApp.Services.CustomManager;

public interface IHistoryManager
{
	Task AppendHistory(NumberConfigurationBase configuration, bool won, decimal? averageSecondsPerEquation);
	Task<List<HistoryModel>> GetHistoryModels(string customLevelKey);
	void ClearHistory(NumberConfigurationBase configuration);
}

public class HistoryManager : IHistoryManager
{
	private readonly IDeviceStorage _storage;

	//TODO: do back of the envelope calculation for storage since there is no paging and data is inteded to append rapidly
	private readonly Dictionary<string, List<HistoryModel>> _historyModels = new();

	public HistoryManager(IDeviceStorage storage)
	{
		_storage = storage;
	}

	public async Task AppendHistory(
		NumberConfigurationBase configuration,
		bool won,
		decimal? averageSecondsPerEquation
    )
	{
		var historyModels = await GetHistoryModels(configuration.ConfigurationKey);

		var lastTryHistoryModel = historyModels
			.Where(x => x.EquationsPerGame == configuration.NumberOfEquations)
			.Where(x => x.SecondsPerEquation == configuration.SecondsPerEquation)
			.Where(x => x.Operations.Equals(configuration.Operations.ToHistoryString()))
			.Where(x => x.Won)
			.OrderByDescending(x => x.Date)
			.FirstOrDefault();

		decimal? improvementInSeconds = null;

		if (lastTryHistoryModel is not null && lastTryHistoryModel.AverageSecondsPerEquation.HasValue && averageSecondsPerEquation.HasValue)
		{
			improvementInSeconds = averageSecondsPerEquation - lastTryHistoryModel.AverageSecondsPerEquation.Value;
		}

		var historyModel = new HistoryModel
		{
			Won = won,
			EquationsPerGame = configuration.NumberOfEquations,
			SecondsPerEquation = configuration.SecondsPerEquation,
			Operations = configuration.Operations.ToHistoryString(),
			AverageSecondsPerEquation = averageSecondsPerEquation,
			ImprovementInSeconds = improvementInSeconds
		};

		historyModels.Insert(0, historyModel);

		await SaveHistoryModels(configuration.ConfigurationKey, historyModels);
	}

	public async Task<List<HistoryModel>> GetHistoryModels(string customLevelKey)
	{
		if (_historyModels.ContainsKey(customLevelKey))
		{
			return _historyModels[customLevelKey];
		}

		var historyModels = await _storage.GetHistoryModels(customLevelKey);
		_historyModels.Add(customLevelKey, historyModels);

		return historyModels;
	}

	public void ClearHistory(NumberConfigurationBase configuration)
	{
		_historyModels.Remove(configuration.ConfigurationKey);
		_storage.Clear(configuration.ConfigurationKey);
	}

	private async Task SaveHistoryModels(string customLevelKey, List<HistoryModel> models)
	{
        _historyModels[customLevelKey] = models;

		await _storage.SaveHistoryModels(customLevelKey, models);
	}
}
