using MentalMathApp.Views;

namespace MentalMathApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MainMenu), typeof(MainMenu));
		Routing.RegisterRoute(nameof(StoryMenu), typeof(StoryMenu));
		Routing.RegisterRoute(nameof(NumberGame), typeof(NumberGame));
		Routing.RegisterRoute(nameof(YouLost), typeof(YouLost));
		Routing.RegisterRoute(nameof(YouWon), typeof(YouWon));
		Routing.RegisterRoute(nameof(NumberCustomMenu), typeof(NumberCustomMenu));
		Routing.RegisterRoute(nameof(NumberCustomLevelConfiguration), typeof(NumberCustomLevelConfiguration));
		Routing.RegisterRoute(nameof(NumberCustomLevelHistory), typeof(NumberCustomLevelHistory));
	}
}
