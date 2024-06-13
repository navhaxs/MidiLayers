using System;
using Melanchall.DryWetMidi.Multimedia;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace InputDeviceExample
{
    class Program
    {
        private static IInputDevice _inputDevice;
        private static IOutputDevice _outputDevice;

        static void Main(string[] args)
        {
            Console.WriteLine("Input Devices:");
            foreach (var d in InputDevice.GetAll())
            {
                Console.WriteLine(d.Name);
            }
            Console.WriteLine("Output Devices:");
            foreach (var outputDevice in OutputDevice.GetAll())
            {
                Console.WriteLine(outputDevice.Name);
            }
            _inputDevice = InputDevice.GetByName("TD-11");
            _inputDevice.EventReceived += OnEventReceived;
            _inputDevice.StartEventsListening();

            _outputDevice = OutputDevice.GetByName("loopMIDI Port Drum Layer");
            _outputDevice.EventSent += OnEventSent;

            _outputDevice.PrepareForEventsSending();

            Console.WriteLine("Input device is listening for events. Press any key to exit...");
            Console.ReadKey();

            (_inputDevice as IDisposable)?.Dispose();
            (_outputDevice as IDisposable)?.Dispose();
        }

        private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            var midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");

            if (e.Event is NoteOnEvent noteOnEvent)
            {
                _outputDevice.SendEvent(new NoteOnEvent((SevenBitNumber)89, noteOnEvent.Velocity));
                _outputDevice.SendEvent(new NoteOffEvent((SevenBitNumber)89, noteOnEvent.Velocity));
            }
            
        }
        
        private static void OnEventSent(object sender, MidiEventSentEventArgs e)
        {
            var midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event sent to '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
        }
    }
}