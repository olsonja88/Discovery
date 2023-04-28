using Godot;
using System;

public partial class main : Node
{
    public int score;

    [Signal]
    public delegate void ResetStopwatchEventHandler();
    [Signal]
    public delegate void UpdateScoreEventHandler(int score);
    
    public override void _Ready()
    {
        score = 0;
        EmitSignal(SignalName.UpdateScore, score);
        // StartGame() function when ready
    }

    private void OnTestLevelFallBoundDeath()
    {
        // GD.Print("Fall Bound Death!");
        HandleDeath();
    }

    private void HandleDeath()
    {
        // GD.Print("Handling Death!");
        GameOver();
    }

    private void RespawnPlayer()
    {
        // GD.Print("Respawning Player!");
        var player = GetNode<CharacterBody2D>("Player");
        var startPos = GetNode<Marker2D>("TestLevel/StartPos");
        player.Position = startPos.Position;
        player.Show();
    }

    private void GameOver()
    {
        // GD.Print("Game Over!");
        var player = GetNode<CharacterBody2D>("Player");
        player.Hide();
        player.QueueFree();
        NewGame();
    }

    private void NewGame()
    {
        // GD.Print("New Game!");
        // Still missing countdown before respawn
        score = 0;
        EmitSignal(SignalName.ResetStopwatch);
        EmitSignal(SignalName.UpdateScore, score);
        RespawnPlayer();
    }

    private void StartGame()
    {
        // Show start screen, call new game when start button is pressed
    }
}
