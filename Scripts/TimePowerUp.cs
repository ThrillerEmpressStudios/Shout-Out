using Godot;
using System;

public class TimePowerUp : Area2D
{
    private Player player;

    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");
        var area = GetNode<Area2D>("Area2D");

        area.Connect("area_entered", this, nameof(OnCollision));

        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/Game/TimePowerUp/Sprite/AnimationPlayer");
        animation.Play("Default");

        var animationNode = animation.GetAnimation("Default");
        animationNode.Loop = true;

    }

    private void OnCollision(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            Game.gameTime += 10;
            QueueFree();
        }
    }
}
