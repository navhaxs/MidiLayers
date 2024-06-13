namespace MidiLayers.Models;

public class MidiMapping
{
    public string Label { get; set; }
    public int NoteNumber { get; set; }

    public MidiMapping(string label, int noteNumber)
    {
        Label = label;
        NoteNumber = noteNumber;
    }
}