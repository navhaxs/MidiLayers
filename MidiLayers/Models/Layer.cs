using System.Runtime.Serialization;
using ReactiveUI;

namespace MidiLayers.Models;

public class Layer : ReactiveObject
{
// TODO change to int 0-127
    private MidiMapping? _input;
    [DataMember]
    public MidiMapping? Input { get => _input; set => this.RaiseAndSetIfChanged(ref _input, value); }
    
    private MidiMapping? _output;
    [DataMember]
    public MidiMapping? Output { get => _output; set => this.RaiseAndSetIfChanged(ref _output, value); }
    
    private string _mod = "";
    [DataMember]
    public string Mod { get => _mod; set => this.RaiseAndSetIfChanged(ref _mod, value); }

    private bool _isMute = false;
    [DataMember]
    public bool IsMute { get => _isMute; set => this.RaiseAndSetIfChanged(ref _isMute, value); }
}