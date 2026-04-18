using Godot;
using System;

public partial class Label : Godot.Label
{
	
	public override void _Ready()
	{
		AddToGroup("points");
	}

	private void updatepoints(float i)
	{
		Text = i.ToString();
	}
}
