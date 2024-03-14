using Godot;
using System;

public partial class ui : CanvasLayer
{
	void OnStartPressed(){
		GetTree().ChangeSceneToFile("res://game.tscn");	
	}

	void OnQuitPressed(){
		GetTree().Quit();
	}
}
