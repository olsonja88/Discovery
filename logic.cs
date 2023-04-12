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
        // Checking player position
        CheckPlayerPos();
        // If player died
        if (playerDied)
        {
            HandleDeath();
        }
    }

    private void CheckPlayerPos()
    {
        // Get Players Y position
        var playerPosY = GetNode<Node2D>("Player").Position.Y;
        // Checking if player is out of screen size bounds
        if (playerPosY > screenSize.Y)
        {
            playerDied = true;
        }
    }

    private void HandleDeath()
    {
        // Getting current number of lives player object
        var playerCurrLives = GetNode<CharacterBody2D>("Player");
        // Subtracting one from the number of lives
    }
}
