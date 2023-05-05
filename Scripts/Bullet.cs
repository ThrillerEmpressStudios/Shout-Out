using Godot;
using System;

public class Bullet : Node2D
{
    public float range = 1024;

    private float distanceTravelled = 0;

    public override void _Ready()
    {
        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, "OnAreaEntered");
    }

    public override void _Process(float delta)
    {
        float speed = 400;
        float moveAmount = speed * delta;

        Position += Transform.x.Normalized() * moveAmount;
        distanceTravelled += moveAmount;

        if (distanceTravelled > range)
            QueueFree();
    }

    private void OnAreaEntered(Node with)
    {
        with.GetParent<Alien>().Damage(10);
        QueueFree();
    }
}
