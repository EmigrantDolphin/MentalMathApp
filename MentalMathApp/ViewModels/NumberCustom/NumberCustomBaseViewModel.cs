using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Navigation;

namespace MentalMathApp.ViewModels.NumberCustom;

public partial class NumberCustomBaseViewModel : BaseViewModel
{
    [RelayCommand]
    public void GoToNumberCustomMenu()
    {
        Navigate.ToNumberCustomMenuAsync(NumberTypes.Integer);
    }
}
