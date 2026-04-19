using Godot;
using System;

public partial class Signal1 : Button
{
	[Export]int i = 1;
	
	public override void _Ready()
	{
		this.Pressed += ButtonPressed;
	}
	private void ButtonPressed()
{
		 GetNode<GameManager>("/root/_gameManager").Signal_pressed(i);
		GD.Print("Push");
	}
}
