using Godot;
using System;

public partial class main : Node
{
    public int score;

    public override void _Ready()
    {

    }

    public void HandleDeath()
    {

    }

    public void RespawnPlayer()
    {
        var player = GetNode<CharacterBody2D>("Player");
        var startPos = GetNode<Marker2D>("TestLevel/StartPos");
        player.Position = startPos.Position;

    }

    public void GameOver()
    {
        var player = GetNode<CharacterBody2D>("Player");
        player.Hide();
    }

    public void NewGame()
    {
        var sw = GetNode<Label>("/UI/Stopwatch");
        sw.Text = "0";
        score = 0;
    }
}
