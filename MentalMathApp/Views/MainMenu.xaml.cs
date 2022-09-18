using MentalMathApp.ViewModels;

namespace MentalMathApp.Views;

public partial class MainMenu : ContentPage
{
	public MainMenu(MainMenuViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

	protected override bool OnBackButtonPressed()
	{
		return true;
	}
}