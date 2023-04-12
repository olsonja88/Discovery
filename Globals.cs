using Godot;
using System;

public partial class Globals : Node
{
    // Number of player lives
    public int playerLives;

    private int GetPlayerLives()
    {
        return playerLives;
    }

    private void SetPlayerLives(int num)
    {
        playerLives = num;
    }
}
