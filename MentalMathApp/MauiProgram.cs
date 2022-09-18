using CommunityToolkit.Maui;
using MentalMathApp.LevelConfigurations.Story;
using MentalMathApp.Services.CustomManager;
using MentalMathApp.Services.Storage;
using MentalMathApp.Services.StoryManager;
using MentalMathApp.ViewModels;
using MentalMathApp.ViewModels.NumberCustom;
using MentalMathApp.Views;

namespace MentalMathApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.AddMentalMathDependencies();
		var app = builder.Build();

		// Loading data from device
		app.Services.GetService<ILevelManager>();
		app.Services.GetService<ICustomLevelManager>();

		return app;
	}

	private static void AddMentalMathDependencies(this MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<ILevelManager, StoryLevelManager>();
		builder.Services.AddSingleton<ICustomLevelManager, CustomLevelManager>();
		builder.Services.AddSingleton<IHistoryManager, HistoryManager>();
		builder.Services.AddSingleton(SecureStorage.Default);
		builder.Services.AddSingleton<IDeviceStorage, DeviceStorage>();
		builder.Services.AddSingleton<IStoryConfigurationFactory, StoryConfigurationFactory>();

		//view models
		builder.Services.AddTransient<YouLostViewModel>();
		builder.Services.AddTransient<YouWonViewModel>();
		builder.Services.AddTransient<MainMenuViewModel>();
		builder.Services.AddTransient<NumberGameViewModel>();
		builder.Services.AddTransient<StoryMenuViewModel>();
		builder.Services.AddTransient<NumberCustomMenuViewModel>();
		builder.Services.AddTransient<NumberCustomConfigurationViewModel>();
		builder.Services.AddTransient<NumberCustomLevelHistoryViewModel>();

		//views

		builder.Services.AddSingleton<MainMenu>();
		builder.Services.AddTransient<StoryMenu>();
		builder.Services.AddTransient<NumberGame>();
		builder.Services.AddTransient<YouLost>();
		builder.Services.AddTransient<YouWon>();
		builder.Services.AddTransient<NumberCustomMenu>();
		builder.Services.AddTransient<NumberCustomLevelConfiguration>();
		builder.Services.AddTransient<NumberCustomLevelHistory>();
	}
}
