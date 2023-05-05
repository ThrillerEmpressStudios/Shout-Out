using Godot;
using System;
using System.Threading.Tasks;

public class MainMenu : Control
{
    private AudioStream clickSound;
    private PackedScene creditsScene;

    public override void _Ready()
    {
        creditsScene = GD.Load<PackedScene>("res://Scenes/Credits.tscn");

        clickSound = GD.Load<AudioStream>("res://Assets/SFX/click_2.wav");
    }

    public async void _on_StartGameButton_pressed()
    {
        ClickButtonSound();
        await Task.Delay(TimeSpan.FromSeconds(0.5));
        GetTree().ChangeScene("res://Scenes/Game.tscn");
    }

    public void _on_CreditsButton_pressed()
    {
        ClickButtonSound();
        var creditsWindow = (Popup)creditsScene.Instance();
        AddChild(creditsWindow);
        creditsWindow.PopupCentered();
    }

    public void _on_QuitGame_pressed()
    {
        ClickButtonSound();
        GetTree().Quit();
    }

    private void ClickButtonSound()
    {
        var clickAudio = GetNode<AudioStreamPlayer>("ClickButton");
        clickAudio.Stream = clickSound;
        clickAudio.Play();
    }
}
