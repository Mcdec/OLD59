using Godot;
using System;

public partial class Ship : Node2D
{
	[Export] public float Speed = 1f;
	[Export] public float RotationSpeed = 3f;
	[Export] public float RotationSmooth = 5f;
	private Node2D Player;
	Vector2 Velocity;
	private float _targetRotation;
	public override void _Ready()
	{
		Player = GetTree().GetFirstNodeInGroup("player") as Node2D;
	}
	public override void _PhysicsProcess(double delta)
	{
		float dt = (float)delta;

		HandleRotationInput(dt);
		ApplySmoothRotation(dt);
		MoveForward(dt);
		GD.Print(GetDistanceToPlayer());
		GD.Print(Position);
		Position += Velocity * dt;
	}

	private void HandleRotationInput(float delta)
	{
		float input = 0;
		_targetRotation += input * RotationSpeed * delta;
	}

	private void ApplySmoothRotation(float delta)
	{
		
		Rotation = Mathf.LerpAngle(Rotation, _targetRotation, RotationSmooth * delta);
	}

	private void MoveForward(float delta)
	{

		Position += new Vector2(1*Speed * delta,1*Speed * delta) ;


	}
	private float GetDistanceToPlayer()
	{
		if (Player == null)
			return float.MaxValue;

		return GlobalPosition.DistanceTo(Player.GlobalPosition);
	}


}
