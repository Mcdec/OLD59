using Godot;
using System;
using System.Diagnostics.SymbolStore;

public partial class MenuManager : Control
{
	
	public void _on_button_pressed()
	{
		GoToNextScene("Game_Sceen_1");
	}
	public void GoToNextScene(string Scene_Name)
	{
		GetTree().ChangeSceneToFile("res://"+Scene_Name+".tscn");
	}
	
}
