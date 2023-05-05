using Godot;
using System;

public class Alien : Node2D
{
    private Timer timer;
    private Player player;

    private float health = 30;

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
            QueueFree();
    }

    public override void _Ready()
    {
        player = GetNode<Player>("../Player");

        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, nameof(OnCollision));
        area.Connect("area_exited", this, nameof(OnCollisionNoMore));

        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, nameof(OnTimerTimeout));
    }

    public override void _Process(float delta)
    {
        float speed = 80;
        float moveAmount = speed * delta;
        Vector2 moveDirection = (player.Position - Position).Normalized();
        Position += moveDirection * moveAmount;
    }

    private void OnCollision(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            timer.Start(1);
        }
    }

    private void OnCollisionNoMore(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            timer.Stop();
        }
    }

    private void OnTimerTimeout()
    {
        if (player != null)
            player.Health -= 20;
    }
}
