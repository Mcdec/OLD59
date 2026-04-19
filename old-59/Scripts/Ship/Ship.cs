using Godot;
using System;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

public partial class Ship : Node2D
{
	Godot.Timer _timer = new Godot.Timer();
	[Export] public float Speed = 1f;
	[Export] public float RotationSpeed = 0.1f;
	[Export] public float RotationSmooth = 5f;
	private float input = 0;
	private Node2D Player;
	Vector2 _velocity;
	private float _targetRotation;
	public override void _Ready()
	{
		AddChild(_timer);
		_timer.WaitTime = 1f;
		_timer.OneShot = false;
		_timer.Connect("timeout", Callable.From(_on_timer_timeout));
		AddToGroup("ship");
		Player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		var _target = Player.Position - Position;
		
		_targetRotation = _target.Angle() + 90f;
		Rotation = _targetRotation;
		_timer.Start(2f);
	}
	public override void _PhysicsProcess(double delta)
	{


		if (GetDistanceToPlayer() > 450) 
		{
			GetNode<GameManager>("/root/_gameManager").Points += 15;
			QueueFree();
		}
		float dt = (float)delta;
		HandleRotationInput(dt);
		ApplySmoothRotation(dt);
		MoveForward(dt);

		Position += _velocity * dt;
	}

	private void HandleRotationInput(float delta)
	{

		_targetRotation += input * RotationSpeed * delta;

	}

	private void ApplySmoothRotation(float delta)
	{

		Rotation = Mathf.LerpAngle(Rotation, _targetRotation, RotationSmooth * delta);
	}

	private void MoveForward(float delta)
	{

		_velocity = Vector2.Up.Rotated(Rotation) * Speed;


	}
	private float GetDistanceToPlayer()
	{
		if (Player == null)
			return float.MaxValue;

		return GlobalPosition.DistanceTo(Player.GlobalPosition);
	}

	private void updaterotation(float i)
	{

		if (_timer.TimeLeft > 0.0001) return;
		input = i;
		_timer.Start(2f);
	}
	private void _on_timer_timeout(){
		_timer.Stop();
		input = 0;}



}
