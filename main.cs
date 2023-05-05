using Godot;
using System;

public partial class main : Node
{
    public int score;
    private int playerLives;

    [Signal]
    public delegate void ResetStopWatchEventHandler();
    [Signal]
    public delegate void UpdateScoreEventHandler(int score);
    [Signal]
    public delegate void UpdateLivesEventHandler(int playerLives);

    public override void _Ready()
    {
        GetTree().Paused = true; // Pause Main and all children
        score = 0; // Set score
        playerLives = 3; // Set lives
        EmitSignal(SignalName.UpdateScore, score); // Update score in UI
        // Hide the player
        var player = GetNode<CharacterBody2D>("Player");
        player.Hide();
    }

    private void OnSpawnTimerTimeout()
    {
        GD.Print("Spawn Timer OUT!");
        // Unpause game
        GetTree().Paused = false;
        RespawnPlayer();
    }

    private void OnTestLevelFallBoundDeath()
    {
        GD.Print("Fall Bound Death!");
        HandleDeath();
    }

    // Once Start Button is pressed
    private void OnUIStartGame()
    {
        GD.Print("UI Start Game Signal Received!");
        NewGame();        
    }

    private void HandleDeath()
    {
        GD.Print("Handling Death!");
        // Hide player
        var player = GetNode<CharacterBody2D>("Player");
        player.Hide();
        GetTree().Paused = true; // Pause Main and all children
        if (playerLives > 0)
        {     
            GD.Print("Player lives > ZERO!");
            playerLives -= 1;
            EmitSignal(SignalName.UpdateLives, playerLives);
            // Start Spawn in timer
            var st = GetNode<Timer>("SpawnTimer");
            st.Start();
        }
        else
        {
            GameOver();
        }        
    }

    private void RespawnPlayer()
    {
        GD.Print("Respawning Player!");        
        var player = GetNode<CharacterBody2D>("Player");
        var startPos = GetNode<Marker2D>("TestLevel/StartPos");
        player.Position = startPos.Position;
        player.Show();        
    }

    private void GameOver()
    {
        GD.Print("Game Over!");        
        // Show Game Message and Start Button
        var sb = GetNode<Button>("UI/StartButton");
        var gm = GetNode<Label>("UI/Message");
        sb.Text = "Retry";
        gm.Text = "Game Over!";
        sb.Show();
        gm.Show();        
    }

    private void NewGame()
    {
        GD.Print("New Game!");
        score = 0;
        EmitSignal(SignalName.ResetStopWatch);
        EmitSignal(SignalName.UpdateScore, score);
        EmitSignal(SignalName.UpdateLives, playerLives);
        // Hide Game Message and Start Button
        var sb = GetNode<Button>("UI/StartButton");
        var gm = GetNode<Label>("UI/Message");
        sb.Hide();
        gm.Hide();
        // Start Spawn in timer
        var st = GetNode<Timer>("SpawnTimer");
        st.Start();
    }
}
