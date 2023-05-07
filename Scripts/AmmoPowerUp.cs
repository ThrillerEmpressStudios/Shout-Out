using Godot;
using System;

public class AmmoPowerUp : Area2D
{
    private Player player;
    private AudioStreamPlayer reloadGunSound;
    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");
        reloadGunSound = GetNode<AudioStreamPlayer>("/root/Game/Player/ReloadingGun");

        Connect("area_entered", this, nameof(OnCollision));
    }

    private void OnCollision(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            player.ammo += 50;
            reloadGunSound.Play();
            QueueFree();
        }
    }
}
