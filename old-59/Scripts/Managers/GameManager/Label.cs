using Godot;
using System;

public partial class Label : Godot.Label
{
	
	public override void _Ready()
	{
		AddToGroup("Lable");
	}

	private void updatescore(float i)
	{
		Text = i.ToString();
	}
}
