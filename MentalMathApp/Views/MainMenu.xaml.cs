using MentalMathApp.ViewModels;

namespace MentalMathApp.Views;

public partial class MainMenu : ContentPage
{
	public MainMenu()
	{
		InitializeComponent();
		BindingContext = new MainMenuViewModel();
	}

	protected override bool OnBackButtonPressed()
	{
		return true;
	}
}