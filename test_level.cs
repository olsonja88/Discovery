using Godot;
using System;

public partial class test_level : Node2D
{
    [Signal]
    public delegate void FallBoundDeathEventHandler();

    public override void _Ready()
    {
        var title = GetNode<Label>("../UI/Message");
        title.Text = "Test Level";
    }

    public void OnFallBoundEntered(Area2D area)
    {
        // GD.Print("Fall Bound Entered!");
        EmitSignal(SignalName.FallBoundDeath);
    }
}
