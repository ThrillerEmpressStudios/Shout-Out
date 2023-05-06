using Godot;
using System;

public class Alien : Node2D
{
    private Timer timer;
    private Player player;

    private float health;
    private float chaseRange;
    private bool chasingPlayer;

    private float speed, moveAmount;

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
            QueueFree();
    }

    public override void _Ready()
    {
        player = GetNode<Player>("/root/Game/Player");
        chasingPlayer = false;

        CollisionShape2D collisionRange = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        var circleShape = (CircleShape2D)collisionRange.Shape;
        circleShape.Radius = 100;

        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, nameof(OnCollision));
        area.Connect("area_exited", this, nameof(ChasePlayer));
    }

    public override void _Process(float delta)
    {
        speed = 80;
        moveAmount = speed * delta;

        if (chasingPlayer)
        {
            ChasePlayer(true);
        }
    }

    private void OnCollision(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            chasingPlayer = true;
        }
    }

    private void ChasePlayer(bool chase)
    {
        if (chase)
        {
            Vector2 moveDirection = (player.Position - Position).Normalized();
            Position += moveDirection * moveAmount;
        }
    }
}
