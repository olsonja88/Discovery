using Godot;
using System;

public partial class stopwatch : Label
{
    float timeElapsed = 0.0f;

    public override void _Process(double delta)
    {
        timeElapsed += (float)delta;

        float minutes = timeElapsed / 60;
        float seconds = timeElapsed % 60;
        float milliseconds = (timeElapsed % 1) * 100;

        // timeString = minutes + ":" + seconds + ":" + milliseconds;
    }
}
