using MentalMathApp.ViewModels.NumberCustom;

namespace MentalMathApp.Views;

public partial class NumberCustomMenu : ContentPage
{
	private readonly NumberCustomMenuViewModel _viewModel;

	public NumberCustomMenu(NumberCustomMenuViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override bool OnBackButtonPressed()
	{
		_viewModel.GoToMainMenuCommand.Execute(this);
		return true;
	}
}