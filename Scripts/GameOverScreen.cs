using Godot;
using System;

public class GameOverScreen : CanvasLayer
{
    public void _on_TryAgain_pressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    }
}
