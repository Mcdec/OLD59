using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading;
using System.Windows.Input;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	
	
	private Node2D Player;
	
	public float IncomePerSecond = 1f;
	Godot.Timer _timer = new Godot.Timer();
	private string _command = null;
	private float angle = 0f;

	private float points = 15;
	private float score = 0;
	public float Points
	{
		get
		{
			return points;
		}
		set {
			points = value;

			GetTree().CallGroup("points", "updatepoints", value);
		}
	}
	public float Score
	{
		get
		{
			return score;
		}
		set
		{
			if (score < 0) score = 0;
			score = value;

			GetTree().CallGroup("Lable", "updatescore", value);
		}
	}
	public float Angle
	{
		get
		{
			return angle;
		}
		set
		{
			angle = value;
			
			GetTree().CallGroup("angle", "updateangle", value);
		}
	}


	public override void _Ready()
	{
		AddChild(_timer);
		_timer.WaitTime = 1f;
		_timer.OneShot = false;
		//	_timer.Timeout += _on_timer_timeout;
		_timer.Connect("timeout", Callable.From(_on_timer_timeout));

		Player = GetTree().GetFirstNodeInGroup("player") as Node2D;
	}
	public override void _Process(double delta)
	{
		Points += IncomePerSecond * (float)delta;
		
	}

	

	

	
	
private void _on_timer_timeout(){
		if (Exists(_command)) {
			Map.TryGetValue(_command, out float angle);
			Angle = angle;
		}
			_command = null;
}

	public static readonly Dictionary<string, float> Map = new()
	{
		{"111",-1f},{"112",-180f},{"113",180f},
		{"121",-140f},{"122",-126.7f},{"123",-113.3f},
		{"131",-100f},{"132",-86.7f},{"133",-73.3f},

		{"211",-60f},{"212",-46.7f},{"213",-33.3f},
		{"221",-20f},{"222",-6.7f},{"223",6.7f},
		{"231",20f},{"232",33.3f},{"233",46.7f},

		{"311",60f},{"312",73.3f},{"313",86.7f},
		{"321",100f},{"322",113.3f},{"323",126.7f},
		{"331",140f},{"332",153.3f},{"333",1f}
	};

	private static bool Exists(string _command)
	{
		return Map.ContainsKey(_command);
	}
	public void Signal_pressed(int i)
	{		
			
			_command += i.ToString();
			if (_command.Length == 1) _timer.Start(1f); 
			if (_command.Length == 3) 
			{
			_timer.Stop();
			if (Exists(_command)) {
				Map.TryGetValue(_command, out float angle);
				Angle = angle;
				
				_command = null;
			}
			
		}		 
	}
}
