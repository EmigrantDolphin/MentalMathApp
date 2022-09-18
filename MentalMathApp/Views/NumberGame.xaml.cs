using MentalMathApp.ViewModels;

namespace MentalMathApp.Views;

public partial class NumberGame : ContentPage
{
	private readonly NumberGameViewModel _viewModel;
	public NumberGame(NumberGameViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
		BindingContext = _viewModel;
	}

	// overrides android back button
	protected override bool OnBackButtonPressed()
	{
		return true;
	}

	protected override void OnDisappearing()
	{
		_viewModel.OnDisappearing();
		base.OnDisappearing();
	}
}