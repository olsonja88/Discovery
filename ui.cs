using Godot;
using System;

public partial class ui : CanvasLayer
{
    [Signal]
    public delegate void StartGameEventHandler();

    public float timeElapsed = 0.0f;

    public override void _Process(double delta)
    {
        UpdateStopwatch(delta);
    }
    
    private void OnMainResetStopWatch()
    {
        // GD.Print("Main StopWatch signal received!");
        ResetStopwatch();
    }

    private void OnMainUpdateScore(int score)
    {
        var sl = GetNode<Label>("Score");
        string scoreString = score.ToString();
        sl.Text = scoreString;
    }

    private void OnStartButtonPressed()
    {
        GD.Print("Start Button pressed!");
        // Emitting custom StartGame signal when StartButton pressed
        EmitSignal(SignalName.StartGame);
    }

    private void ResetStopwatch()
    {
        // GD.Print("Resetting stopwatch!");
        timeElapsed = 0.0f;
    }

    private void UpdateStopwatch(double delta)
    {
        var sw = GetNode<Label>("Stopwatch");
        timeElapsed += (float)delta;

        float minutes = timeElapsed / 60;
        float seconds = timeElapsed % 60;
        float milliseconds = (timeElapsed % 1) * 100;

        string timeString = (int)minutes + ":" + (int)seconds + ":" + (int)milliseconds;
        sw.Text = timeString;
    }

    private void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();
    }

    private void ShowGameOver()
    {
        ShowMessage("Game Over");
    }

}
