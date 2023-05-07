using Godot;
using System;

public class Player : Node2D
{
    PackedScene bulletScene;
    CanvasLayer gameOverScreen, pauseMenuScene;
    Node2D gameRoot;

    public int ammo;
    private float health = 100;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    [Export]
    public float speed = 200;

    private Vector2 _direction = new Vector2();

    public override void _Ready()
    {
        ammo = 40;

        bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");

        Position = new Vector2(52, 900);
    }

    public override void _PhysicsProcess(float delta)
    {
        Rotation = (GetGlobalMousePosition() - GlobalPosition).Angle();

        _direction = new Vector2();

        if (Input.IsActionPressed("up"))
        {
            _direction.y -= 1;
        }
        if (Input.IsActionPressed("down"))
        {
            _direction.y += 1;
        }
        if (Input.IsActionPressed("left"))
        {
            _direction.x -= 1;
        }
        if (Input.IsActionPressed("right"))
        {
            _direction.x += 1;
        }

        _direction = _direction.Normalized();

        Position += _direction * speed * delta;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (Input.IsActionJustPressed("shoot"))
            {
                Shoot();
            }
        }
        else if (@event is InputEventJoypadButton joystickEvent)
        {
            if (Input.IsActionJustPressed("shoot_control"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        AudioStreamPlayer shootSound = GetNode<AudioStreamPlayer>("ShootSound");
        if (ammo > 0)
        {
            shootSound.Play();
            Bullet bullet = (Bullet)bulletScene.Instance();
            bullet.Position = Position;
            bullet.Rotation = Rotation;

            GetParent().AddChild(bullet);
            GetTree().SetInputAsHandled();

            ammo -= 1;
        }
    }
}
