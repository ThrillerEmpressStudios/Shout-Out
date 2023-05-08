using Godot;
using System;

public class Alien : Node2D
{
    private Area2D area;
    private PackedScene bulletScene;
    private Timer timer;
    private Player player;
    private bool chasingPlayer, chase, superPowerOn;

    private float health, speed, moveAmount, rotationSpeed;
    public float chaseRange;
    private int alienType;

    public void SetAlienType(int alienType)
    {
        switch (alienType)
        {
            case 1:
                area = GetNode<Area2D>("Area2D");
                area.Connect("area_entered", this, nameof(OnCollision));
                area.Connect("area_exited", this, nameof(NotLookingPlayer));

                this.alienType = 1;
                break;
            case 2:
                area = GetNode<Area2D>("Area2D");
                area.Connect("area_entered", this, nameof(OnCollision));
                area.Connect("area_exited", this, nameof(NotLookingPlayer));

                this.alienType = 2;
                break;
            case 3:
                area = GetNode<Area2D>("Area2D");
                area.Connect("area_entered", this, nameof(OnCollision));
                area.Connect("area_exited", this, nameof(NotLookingPlayer));

                this.alienType = 3;
                break;
            default:
                GD.Print("It is not a valid type");
                break;
        }
    }

    public override void _Ready()
    {
        rotationSpeed = 0;
        superPowerOn = false;
        timer = GetNode<Timer>("ShootTime");

        bulletScene = GD.Load<PackedScene>("res://Scenes/AlienBullet.tscn");

        player = GetNode<Player>("/root/Game/Player");
        chasingPlayer = false;

        CollisionShape2D collisionRange = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        var circleShape = (CircleShape2D)collisionRange.Shape;
        circleShape.Radius = 200;
    }

    public override void _Process(float delta)
    {
        speed = 80;
        moveAmount = speed * delta;

        if (chasingPlayer)
        {
            LookAtPlayer(chasingPlayer);
        }
        if (chase)
        {
            Vector2 moveDirection = (player.Position - Position).Normalized();
            Position += moveDirection * moveAmount;
        }
        if (superPowerOn)
        {
            SuperPower(rotationSpeed, delta);
            if (rotationSpeed >= 8.0)
            {
                ShootingPlayer();
            }
        }
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
            QueueFree();
    }

    private void OnCollision(Area2D with)
    {
        if (alienType == 1)
        {
            if (with.GetParent() is Player player)
            {
                chasingPlayer = true;
                timer.Start();
                timer.Connect("timeout", this, nameof(ShootingPlayer));
            }
        }
        else if (alienType == 2)
        {
            if (with.GetParent() is Player player)
            {
                timer.Start((float)0.5);
                superPowerOn = true;
                timer.Connect("timeout", this, nameof(Spin));
            }
        }
        else if (alienType == 3)
        {
            if (with.GetParent() is Player player)
            {
                chase = true;
                chasingPlayer = true;
                timer.Start();
                timer.Connect("timeout", this, nameof(ShootingPlayer));
            }
        }
    }

    private void NotLookingPlayer(Area2D with)
    {
        if (with.GetParent() is Player player)
        {
            superPowerOn = false;
            rotationSpeed = 0;
            chasingPlayer = false;
            chase = false;
            timer.Stop();
        }
    }

    private void LookAtPlayer(bool chase)
    {
        if (chase)
        {
            var playerPosition = player.Position;
            LookAt(playerPosition);
        }
    }

    private void SuperPower(float rotationSpeed, float time)
    {
        Rotation += rotationSpeed * time;
        if (Rotation > Mathf.Pi * 2)
        {
            Rotation -= Mathf.Pi * 2;
        }
    }

    private void Spin()
    {
        rotationSpeed += (float)1.0;
    }

    private void ShootingPlayer()
    {
        AudioStreamPlayer shootSound = GetNode<AudioStreamPlayer>("ShootSound");
        shootSound.Play();
        AlienBullet bullet = (AlienBullet)bulletScene.Instance();
        bullet.Position = Position;
        bullet.Rotation = Rotation;

        GetParent().AddChild(bullet);
    }
}
