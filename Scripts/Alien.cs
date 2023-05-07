using Godot;
using System;

public class Alien : Node2D
{
    private PackedScene bulletScene;
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
        circleShape.Radius = 200;

        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, nameof(OnCollision));
        area.Connect("area_exited", this, nameof(NotLookingPlayer));
    }

    public override void _Process(float delta)
    {
        speed = 80;
        moveAmount = speed * delta;

        if (chasingPlayer)
        {
            LookAtPlayer(true);
        }
    }

    private void OnCollision(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            chasingPlayer = true;
        }
    }

    private void NotLookingPlayer(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            chasingPlayer = false;
        }
    }

    private void LookAtPlayer(bool chase)
    {
        if (chase)
        {
            var playerPosition = player.Position;
            LookAt(playerPosition);
            ShootingPlayer();
        }
    }

    private void ShootingPlayer()
    {
    }
}
