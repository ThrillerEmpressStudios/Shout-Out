using Godot;
using System;

public class Player : KinematicBody2D
{
    PackedScene bulletScene;

    [Export]
    public float speed = 200;

    private Vector2 _direction = new Vector2();

    public override void _Ready()
    {
        bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");

        Position = new Vector2(512, 300);
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
                Bullet bullet = (Bullet)bulletScene.Instance();
                bullet.Position = Position;
                bullet.Rotation = Rotation;

                GetParent().AddChild(bullet);
                GetTree().SetInputAsHandled();
            }
        }
        else if (@event is InputEventJoypadButton joystickEvent)
        {
            if (Input.IsActionJustPressed("shoot_control"))
            {
                Bullet bullet = (Bullet)bulletScene.Instance();
                bullet.Position = Position;
                bullet.Rotation = Rotation;

                GetParent().AddChild(bullet);
                GetTree().SetInputAsHandled();
            }
        }
    }

}
