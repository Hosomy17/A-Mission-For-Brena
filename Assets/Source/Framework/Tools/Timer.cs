using UnityEngine;
using System.Collections;

public class Timer
{
    private float time;

    public Timer()
    {
        this.time = Time.time;
    }

    public Timer(float time)
    {
        this.time = Time.time + time;
    }

    public void Reset()
    {
        this.time = Time.time;
    }

    public void Reset(float time)
    {
        this.time = Time.time + time;
    }

    public float GetTime()
    {
        return Time.time - this.time;
    }
}