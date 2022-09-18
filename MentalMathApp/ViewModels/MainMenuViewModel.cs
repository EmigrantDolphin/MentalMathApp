using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Navigation;

namespace MentalMathApp.ViewModels;

public partial class MainMenuViewModel : BaseViewModel
{
    [RelayCommand]
    public void GoToIntegerNumberCustomMenu()
    {
        Navigate.ToNumberCustomMenu(NumberTypes.Integer);
    }

    [RelayCommand]
    public void GoToRationalNumberCustomMenu()
    {
        Navigate.ToNumberCustomMenu(NumberTypes.Rational);
    }
}
