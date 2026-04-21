using Godot;
using System;
using System.Threading;

public partial class DDraw : Sprite2D
{
	Color color = new Color("baea55ff");
	float _radarradius = 10;
	public float Radar_radius
	{
		get
		{
			return _radarradius;
		}

		set
		{
			_radarradius = value;
			
			QueueRedraw();
		}
	}
	public float Color_A
	{
		get
		{
			return color.A;
		}

		set
		{
			color.A = value;
			if (color.A == 0) QueueFree();
		}
	}
	
	public override void _Ready()
	{
	
		var tween = CreateTween();
		_radarradius = 0f;
		color.A = 1f;
		tween.Parallel();
		tween.TweenProperty(this, "Radar_radius", 2048f, 1f);
		tween.TweenProperty(this, "Color_A", 0f, 0.1f);
		

	}

	
	public override void _Draw()
	{

		DrawCircle(Vector2.Zero, _radarradius, color, false, 50f, true);

	}
}
