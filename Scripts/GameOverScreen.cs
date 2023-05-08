using Godot;
using System;

public class GameOverScreen : CanvasLayer
{
    Game mainGame;
    Player player;
    AudioStreamPlayer loseSound;
    public void _on_TryAgain_pressed()
    {
        mainGame = GetNode<Game>("/root/Game");
        mainGame.RestartGame();
        GetTree().ChangeScene("res://Scenes/Game.tscn");
        QueueFree();
    }

    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");
        loseSound = GetNode<AudioStreamPlayer>("/root/Game/GameOverScreen/AudioStreamPlayer");
    }

    public override void _Process(float delta)
    {
        if (player.Health <= 0)
        {
            loseSound.Play();
        }
    }
}
