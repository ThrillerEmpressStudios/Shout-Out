using Godot;
using System;

public class PlayerHealth : Label
{
    public override void _Process(float delta)
    {
        var player = GetNode<Player>("/root/Game/Player");

        if (player != null)
        {
            Text = $"Health: {player.Health}";
        }
        else
        {
            Text = "Player does not exist";
        }

        if (player.Health <= 10)
        {
            Modulate = new Color(1, 0, 0);
        }
    }
}
