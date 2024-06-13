using System;
using System.Collections.ObjectModel;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using MidiLayers.Models;
using MidiLayers.ViewModels;

namespace MidiLayers.Services;

public class MidiLayerEngine : IDisposable
{
    private static IInputDevice _inputDevice;
    private static IOutputDevice _outputDevice;

    private ObservableCollection<Layer> _layers;

    public MidiLayerEngine(ref ObservableCollection<Layer> layers)
    {
        _layers = layers;

        _inputDevice = InputDevice.GetByName("TD-11");
        _inputDevice.EventReceived += OnEventReceived;
        _inputDevice.StartEventsListening();

        _outputDevice = OutputDevice.GetByName("loopMIDI Port Drum Layer");
        _outputDevice.EventSent += OnEventSent;

        _outputDevice.PrepareForEventsSending();
    }

    private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        var midiDevice = (MidiDevice)sender;
        Console.WriteLine($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");

        if (e.Event is NoteOnEvent noteOnEvent)
        {
            foreach (var layer in _layers.Where(layer => !layer.IsMute && layer.Input?.NoteNumber == noteOnEvent.NoteNumber))
            {
                if (layer.Output != null)
                {
                    _outputDevice.SendEvent(new NoteOnEvent((SevenBitNumber)layer.Output.NoteNumber, noteOnEvent.Velocity));
                    _outputDevice.SendEvent(new NoteOffEvent((SevenBitNumber)layer.Output.NoteNumber, noteOnEvent.Velocity));
                }
            }
        }
    }

    private static void OnEventSent(object sender, MidiEventSentEventArgs e)
    {
        var midiDevice = (MidiDevice)sender;
        Console.WriteLine($"Event sent to '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
    }

    public void Dispose()
    {
        (_inputDevice as IDisposable)?.Dispose();
        (_outputDevice as IDisposable)?.Dispose();
    }
}