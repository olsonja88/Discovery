using Godot;
using System;

public partial class logic : Node
{
    public override void _Ready()
    {
        // Get player node on start-up
        var playerInit = GetNode<CharacterBody2D>("Player");
    }
}
