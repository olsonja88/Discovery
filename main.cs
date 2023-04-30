using Godot;
using System;

public partial class main : Node
{
    public int score;

    [Signal]
    public delegate void ResetStopWatchEventHandler();
    [Signal]
    public delegate void UpdateScoreEventHandler(int score);

    public override void _Ready()
    {
        GetTree().Paused = true; // Pause Main and all children on Ready
        score = 0; // Set score
        EmitSignal(SignalName.UpdateScore, score); // Update score in UI
    }

    private void OnTestLevelFallBoundDeath()
    {
        // GD.Print("Fall Bound Death!");
        HandleDeath();
    }

    private void OnUIStartGame()
    {
        GetTree().Paused = false;
        var sb = GetNode<Button>("UI/StartButton");
        var gm = GetNode<Label>("UI/Message");
        sb.Hide();
        gm.Hide();
        GD.Print("UI Start Game Signal Received!");
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
        EmitSignal(SignalName.ResetStopWatch);
        EmitSignal(SignalName.UpdateScore, score);
        RespawnPlayer();
    }
}
