using System;

public class TimingPoints
{
    private int offset;
    private double msPerBeat;
    private int beatsPerMeasure;
    private int volume;
    private int playmode;

    public TimingPoints(int offset, double msPerBeat, int beatsPerMeasure, int volume, int playmode)
    {
        this.offset = offset;
        this.msPerBeat = msPerBeat;
        this.beatsPerMeasure = beatsPerMeasure;
        this.volume = volume;
        this.playmode = playmode;
    }
}
