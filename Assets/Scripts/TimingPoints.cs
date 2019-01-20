﻿using System;

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
        this.playmode = playmode; //0 is ring mode, 1 is simon says mode
    }

    public int getOffset()
    {
        return offset;
    }
    public double getMsPerBeat()
    {
        return msPerBeat;
    }
    public int getBeatsPerMeasure()
    {
        return beatsPerMeasure;
    }
    public int getVolume()
    {
        return volume;
    }

    public int getPlaymode()
    {
        return playmode;
    }
}
