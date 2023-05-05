using Godot;
using System;

public class Game : Node2D
{
    Player player;
    Timer gameTimer;
    Label gameTimeText, playerAmmoText;
    private int gameTime = 100;

    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");

        gameTimer = GetNode<Timer>("/root/Game/GameTimer");
        gameTimeText = GetNode<Label>("/root/Game/Player UI/VBoxContainer2/GameTime");
        playerAmmoText = GetNode<Label>("/root/Game/Player UI/VBoxContainer3/PlayerAmmunition");

        gameTimer.Start();
    }

    public override void _Process(float delta)
    {
        gameTimeText.Text = $"Time: {gameTime}";
        playerAmmoText.Text = $"Ammo: {player.ammo}";

        if (gameTime <= 0)
        {
            gameTimer.Stop();
        }

        if (gameTime <= 30 && gameTime > 9)
        {
            gameTimeText.Modulate = new Color(255, 255, 0);
            
            if (gameTime <= 10)
            {
                gameTimeText.Modulate = new Color(1, 0, 0);
            }
        }

    }

    public void _on_GameTimer_timeout()
    {
        gameTime -= 1;
    }
}