using Godot;
using System;

public partial class main : Node
{
    public int score;

    [Signal]
    public delegate void StopWatchEventHandler();
    [Signal]
    public delegate void UpdateScoreEventHandler(int score);

    public override void _Ready()
    {
        score = 0;
        EmitSignal(SignalName.UpdateScore, score);
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
        NewGame();
    }

    private void NewGame()
    {
        // GD.Print("New Game!");
        score = 0;
        EmitSignal(SignalName.StopWatch);
        EmitSignal(SignalName.UpdateScore, score);
        RespawnPlayer();
    }
}
