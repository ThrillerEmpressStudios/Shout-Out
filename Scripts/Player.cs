using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public float speed = 200;

    private Vector2 _direction = new Vector2();

    public override void _Ready()
    {
        Position = new Vector2(512, 300);
    }

    public override void _PhysicsProcess(float delta)
    {
        LookAt(GetGlobalMousePosition());

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
}
