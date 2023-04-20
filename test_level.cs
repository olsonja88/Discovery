using Godot;
using System;

public partial class test_level : Node2D
{
    [Signal]
    public delegate void FallBoundDeathEventHandler();

    public void OnFallBoundEntered(Area2D area)
    {
        GD.Print("Fall Bound Entered!");
        EmitSignal(SignalName.FallBoundDeath);
    }
}
