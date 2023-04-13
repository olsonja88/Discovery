using Godot;
using System;

public partial class logic : Node2D
{
    // New vector for game window screenSize
    public Vector2 screenSize;
    // Bool for if the player just died
    private bool playerDied;

    public override void _Ready()
    {
        // Get player node on start-up
        var playerInit = GetNode<CharacterBody2D>("Player");
        // Set screenSize to Screen Size
        screenSize = GetViewportRect().Size;
        // Init justDied
        playerDied = false;
    }

    public override void _Process(double Delta)
    {
    }

    private void CheckPlayerPos()
    {
    }

    private void HandleDeath()
    {
    }
}
