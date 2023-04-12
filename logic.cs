using Godot;
using System;

public partial class logic : Node2D
{
    // New vector for game window screenSize
    public Vector2 screenSize;

    public override void _Ready()
    {
        // Get player node on start-up
        var playerInit = GetNode<CharacterBody2D>("Player");
        // Set screenSize to Screen Size
        screenSize = GetViewportRect().Size;
    }

    public override void _Process(double Delta)
    {
        
    }

    private void CheckPlayerPos()
    {
        var playerPos = GetNode<Node2D>("Player").Position.Y;
    }

    private void HandleDeath()
    {

    }
}
