using MentalMathApp.ViewModels;

namespace MentalMathApp.Views;

public partial class StoryMenu : ContentPage
{
	private readonly StoryMenuViewModel _viewModel;

	public StoryMenu(StoryMenuViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel;
	}

	// overrides android back button
	protected override bool OnBackButtonPressed()
	{
		_viewModel.GoToMainMenuCommand.Execute(this);
		return true;
	}
}