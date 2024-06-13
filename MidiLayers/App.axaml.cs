using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MidiLayers.ViewModels;
using ReactiveUI;

namespace MidiLayers;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public const string APP_STATE_FILEPATH = "appstate.json";

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Create the AutoSuspendHelper.
            var suspension = new AutoSuspendHelper(desktop);
            RxApp.SuspensionHost.CreateNewAppState = () => new MainViewModel();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(new NewtonsoftJsonSuspensionDriver<MainViewModel>(APP_STATE_FILEPATH));
            suspension.OnFrameworkInitializationCompleted();

            // Load the saved view model state.
            var mainViewModel = RxApp.SuspensionHost.GetAppState<MainViewModel>();
            
            desktop.MainWindow = new MainWindow() { DataContext = mainViewModel };
        }

        base.OnFrameworkInitializationCompleted();
    }
}