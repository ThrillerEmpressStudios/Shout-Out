using Godot;
using System;

public class GameOverScreen : CanvasLayer
{
    Game mainGame;
    public void _on_TryAgain_pressed()
    {
        mainGame = GetNode<Game>("/root/Game");
        mainGame.RestartGame();
        GetTree().ChangeScene("res://Scenes/Game.tscn");
        QueueFree();
    }
}
