using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Avalonia.Controls;
using MidiLayers.Models;
using MidiLayers.Services;
using ReactiveUI;

namespace MidiLayers.ViewModels;

public class MainViewModel : ReactiveObject
{
    public MidiLayerEngine MidiLayerEngine;
    
    public List<MidiMapping> InputMappings { get; init; }
    public List<MidiMapping> OutputMappings { get; init; }
    public MainViewModel()
    {
        if (Design.IsDesignMode)
            return;
        
        MidiLayerEngine = new MidiLayerEngine(ref _layers);

        InputMappings = new List<MidiMapping>(Constants.InputKitPieces);
        OutputMappings = new List<MidiMapping>(Constants.OutputKitPieces);
    }

    private ObservableCollection<Layer> _layers = new ObservableCollection<Layer>() { };

    [DataMember]
    public ObservableCollection<Layer> Layers
    {
        get => _layers;
        set => this.RaiseAndSetIfChanged(ref _layers, value);
    }
}