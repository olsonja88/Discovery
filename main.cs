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
        GetTree().Paused = true; // Pause Main and all children
        score = 0; // Set score
        EmitSignal(SignalName.UpdateScore, score); // Update score in UI
        // Hide the player
        var player = GetNode<CharacterBody2D>("Player");
        player.Hide();
    }

    private void OnSpawnTimerTimeout()
    {
        // Unpause game
        GetTree().Paused = false;
        // Show player
        var player = GetNode<CharacterBody2D>("Player");
        player.Show();
    }

    private void OnTestLevelFallBoundDeath()
    {
        // GD.Print("Fall Bound Death!");
        HandleDeath();
    }

    // Once Start Button is pressed
    private void OnUIStartGame()
    {
        GD.Print("UI Start Game Signal Received!");
        // Hide Game Message and Start Button
        var sb = GetNode<Button>("UI/StartButton");
        var gm = GetNode<Label>("UI/Message");
        sb.Hide();
        gm.Hide();
        // Start Spawn in timer
        var st = GetNode<Timer>("SpawnTimer");
        st.Start();
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
