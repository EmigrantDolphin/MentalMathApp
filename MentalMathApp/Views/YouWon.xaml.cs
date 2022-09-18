using MentalMathApp.ViewModels;

namespace MentalMathApp.Views;

public partial class YouWon : ContentPage
{
	public YouWon(YouWonViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	// overrides android back button
	protected override bool OnBackButtonPressed()
	{
		return true;
	}
}