using Godot;
using System;

public class AlienBullet : Node2D
{
    private float range = 1024;
    private float distanceTravelled = 0;

    private AudioStreamPlayer shootSound;
    
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
        with.GetParent<Player>().Health -= 5;
        QueueFree();
    }
}