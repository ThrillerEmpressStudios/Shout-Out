using Godot;
using System;

public class MainMenu : Control
{
    public override void _Ready()
    {
        OS.WindowResizable = false;
        OS.SetWindowTitle("");
    }

    public void _on_StartGameButton_pressed()
    {
        GetTree().ChangeScene("res://Scenes/Game.tscn");
    }

    public void _on_QuitGame_pressed()
    {
        GetTree().Quit();
    }
}
