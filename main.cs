using Godot;
using System;

public partial class main : Node
{
    private int _score;

    public override void _Ready()
    {

    }

    public void HandleDeath()
    {

    }

    public void RespawnPlayer()
    {

    }

    public void GameOver()
    {
      
    }

    public void NewGame()
    {
        var sw = GetNode<Label>("Stopwatch");
        sw.Text = "0";
        _score = 0;
    }
}
