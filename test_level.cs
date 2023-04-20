using Godot;
using System;

public partial class test_level : Node2D
{
    public void OnFallBoundEntered(Area2D area)
    {
        GD.Print("Fall Bound Entered!");
        var player = GetNode<CharacterBody2D>("../Player");
        player.Hide();
    }
}
