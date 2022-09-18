using MentalMathApp.ViewModels.NumberCustom;

namespace MentalMathApp.Views;

public partial class NumberCustomLevelConfiguration : ContentPage
{
	private readonly NumberCustomConfigurationViewModel _viewModel;

	public NumberCustomLevelConfiguration(NumberCustomConfigurationViewModel viewModel)
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