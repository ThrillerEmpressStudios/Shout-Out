using Godot;
using System;

public class PauseMenu : CanvasLayer
{
    private AudioStream clickSound;

    public override void _Ready()
    {
        GetTree().Paused = false;
        Visible = false;
        clickSound = GD.Load<AudioStream>("res://Assets/SFX/click_2.wav");
    }

    public void _on_ResumeGame_pressed()
    {
        ClickButtonSound();
        SetPause(false);
    }

    public void _on_QuitGame2_pressed()
    {
        ClickButtonSound();
        GetTree().Quit();
    }

    private void ClickButtonSound()
    {
        var clickAudio = GetNode<AudioStreamPlayer>("/root/Game/PauseMenu/Control/ClickButton");
        clickAudio.Stream = clickSound;
        clickAudio.Play();
    }

    private void SetPause(bool pause)
    {
        GetTree().Paused = pause;
        Visible = pause;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            SetPause(true);
        }
    }
}
