using Godot;
using System;

public class VictoryScreen : CanvasLayer
{
    Game mainGame;
    Timer timer;
    AudioStreamPlayer winSound;
    public static bool playerWin;
    public override void _Ready()
    {
        playerWin = false;

        winSound = GetNode<AudioStreamPlayer>("/root/Game/VictoryScreen/AudioStreamPlayer");
        timer = GetNode<Timer>("/root/Game/VictoryScreen/Timer");
    }

    public override void _Process(float delta)
    {
        if (playerWin)
        {
            timer.Start((float)2.5);
            winSound.Play();
            playerWin = false;
        }
    }
    public void _on_Timer_timeout()
    {
        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    }
}
