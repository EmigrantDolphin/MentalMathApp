using MentalMathApp.ViewModels.NumberCustom;

namespace MentalMathApp.Views;

public partial class NumberCustomLevelHistory : ContentPage
{
	private readonly NumberCustomLevelHistoryViewModel _viewModel;

	public NumberCustomLevelHistory(NumberCustomLevelHistoryViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override bool OnBackButtonPressed()
	{
		_viewModel.GoBackCommand.Execute(this);
		return true;
	}
}