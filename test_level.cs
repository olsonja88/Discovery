using Godot;
using System;

public partial class test_level : Node2D
{
    public void OnArea2DEntered(Area2D area)
    {
        GD.Print("Fall Bound Entered!");
    }
}
