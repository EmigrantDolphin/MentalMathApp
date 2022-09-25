using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.Navigation;

namespace MentalMathApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy => !isBusy;

    private List<CancellationTokenSource> cancellationSources = new();

    [RelayCommand]
    private void GoToStoryMenu()
    {
        _ = Navigate.ToStoryMenuAsync();
    }

    [RelayCommand]
    public async Task GoBackAsync()
    {
        await Navigate.GoBackAsync();
    }

    [RelayCommand]
    private async Task GoToMainMenuAsync()
    {
        await Navigate.ToMainMenuAsync();
    }

    public CancellationToken CreateCancellationToken()
    {
        var source = new CancellationTokenSource();
        cancellationSources.Add(source);

        return source.Token;
    }
    public void CancelTasks()
    {
        foreach(var source in cancellationSources)
        {
            source.Cancel();
        }

        cancellationSources.Clear();
    }
}
