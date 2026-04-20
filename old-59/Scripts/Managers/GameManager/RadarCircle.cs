using Godot;
using System;

public partial class RadarCircle : Sprite2D
{
	
	public override void _Ready()
	{
		
		AddToGroup("RadarCircle");
		
	}
	void RadarStart()
	{
		var tween = CreateTween();
		
		tween.TweenProperty(this,"scale", new Vector2(25,25), 10);
		GD.Print(Scale);
		tween.TweenProperty(this,"scale", new Vector2(0,0), 0);
	}
}
