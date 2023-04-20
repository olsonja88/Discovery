using Godot;
using System;

public partial class main : Node
{
    public int score;

    public override void _Ready()
    {

    }

    public void OnTestLevelFallBoundDeath()
    {
        GD.Print("Fall Bound Death!");
        HandleDeath();
    }

    public void HandleDeath()
    {
        GD.Print("Handling Death!");
        GameOver();
    }

    public void RespawnPlayer()
    {
        GD.Print("Respawning Player!");
        var player = GetNode<CharacterBody2D>("Player");
        var startPos = GetNode<Marker2D>("TestLevel/StartPos");
        player.Position = startPos.Position;
        player.Show();
    }

    public void GameOver()
    {
        GD.Print("Game Over!");
        var player = GetNode<CharacterBody2D>("Player");
        player.Hide();
        NewGame();
    }

    public void NewGame()
    {
        GD.Print("New Game!");
        var sw = GetNode<Label>("UI/Stopwatch");
        sw.Text = "0";
        score = 0;
        RespawnPlayer();
    }
}
