using Avalonia.Controls;
using Avalonia.Interactivity;
using MidiLayers.Models;
using MidiLayers.ViewModels;

namespace MidiLayers;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.Closing += (sender, args) =>
        {
            if (this.DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.MidiLayerEngine.Dispose();
            }
        };
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.Layers.Add(new Layer());
        }
    }

    private void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Control control)
        {
            if (control.DataContext is Layer layer)
            {
                if (this.DataContext is MainViewModel mainViewModel)
                {
                    mainViewModel.Layers.Remove(layer);
                }
            }
        }
    }
}