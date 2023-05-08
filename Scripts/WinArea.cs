using Godot;
using System;

public class WinArea : Area2D
{
    CanvasLayer wonScene, pauseMenu;
    
    public override void _Ready()
    {
        Connect("area_entered", this, "OnAreaEntered");
    }

    private void OnAreaEntered(Node with)
    {
        wonScene = GetNode<CanvasLayer>("/root/Game/VictoryScreen");
        wonScene.Visible = true;

        pauseMenu = GetNode<CanvasLayer>("/root/Game/PauseMenu");
        pauseMenu.QueueFree();

        VictoryScreen.playerWin = true;
    }
}
