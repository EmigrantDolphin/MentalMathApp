using CommunityToolkit.Mvvm.Input;

namespace MentalMathApp.Components;

public partial class ShadowButton : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ShadowButton), string.Empty);
    public new static readonly BindableProperty StyleProperty =
        BindableProperty.Create(nameof(Style), typeof(Style), typeof(ShadowButton), null);
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(IAsyncRelayCommand), typeof(ShadowButton), null);
    // This is bound to IsEnabled of a button, but it doesn't care. Button is still always enabled. Scrabbing this shadow component, not bothered enough to figure this out
    public new static readonly BindableProperty IsEnableddProperty =
        BindableProperty.Create(nameof(IsEnabledd), typeof(bool), typeof(ShadowButton), false);

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public new Style Style
    {
        get { return (Style)GetValue(StyleProperty); }
        set { SetValue(StyleProperty, value); }
    }
    public IAsyncRelayCommand Command
    {
        get { return IsEnabledd ? (IAsyncRelayCommand)GetValue(CommandProperty) : null; }
        set { SetValue(CommandProperty, value); }
    }
    public new bool IsEnabledd
    {
        get { return (bool)GetValue(IsEnableddProperty); }
        set { SetValue(IsEnableddProperty, value); }
    }

	public ShadowButton()
	{
		InitializeComponent();
	}
}