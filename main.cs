using Godot;
using System;

public partial class main : Node
{
    private int _score;

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
