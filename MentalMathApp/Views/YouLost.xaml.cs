using MentalMathApp.ViewModels;

namespace MentalMathApp.Views;

public partial class YouLost : ContentPage
{
	public YouLost(YouLostViewModel viewModel)
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