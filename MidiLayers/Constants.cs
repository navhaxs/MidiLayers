using Melanchall.DryWetMidi.Common;
using MidiLayers.Models;

namespace MidiLayers;

public static class Constants
{
    public static MidiMapping[] InputKitPieces = new[]
    {
        new MidiMapping("HH Foot Close", (SevenBitNumber)44),
        new MidiMapping("CC Hihat Bell", (SevenBitNumber)9),
        new MidiMapping("Snare Open Hit", (SevenBitNumber)38)
    };
    
    public static MidiMapping[] OutputKitPieces = new[]
    {
        new MidiMapping("Flexi 1 Hit A", (SevenBitNumber)89),
        // new MidiMapping("CC Hihat Shaft", (SevenBitNumber)26),
        // new MidiMapping("Snare Open Hit", (SevenBitNumber)84)
    };
}