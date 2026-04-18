using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class Ship : Node2D
{
	[Export] public float Speed = 1f;
	[Export] public float RotationSpeed = 3f;
	[Export] public float RotationSmooth = 5f;
	private float input = 0;
	private Node2D Player;
	Vector2 _velocity;
	private float _targetRotation;
	public override void _Ready()
	{
		AddToGroup("ship");
		Player = GetTree().GetFirstNodeInGroup("player") as Node2D;
	}
	public override void _PhysicsProcess(double delta)
	{
		float dt = (float)delta;
		ApplySmoothRotation(dt, input);
		MoveForward(dt);
		
		Position += _velocity * dt;
	}

	

	private void ApplySmoothRotation(float delta, float input)
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

		_targetRotation += i;
		GD.Print("2 " + _targetRotation);
	}
}
