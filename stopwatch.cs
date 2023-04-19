using Godot;
using System;

public partial class stopwatch : Label
{
    private float timeElapsed = 0.0f;

    public override void _Process(double delta)
    {
        UpdateStopwatch(delta);
    }

    private void UpdateStopwatch(double delta)
    {
        timeElapsed += (float)delta;

        float minutes = timeElapsed / 60;
        float seconds = timeElapsed % 60;
        float milliseconds = (timeElapsed % 1) * 100;

        string timeString = (int)minutes + ":" + (int)seconds + ":" + (int)milliseconds;
        Text = timeString;
    }
}
