using Godot;
using System;
using System.Reflection.Emit;

public partial class GameManager : Node2D
{
	[Export] public PackedScene ShipPrefab;
	private Node2D Player;
	[Export] public float SpawnCost = 1000f;
	[Export] public float IncomePerSecond = 1f;
	
	private float _points = 999;
	[Export] public Godot.Label _label;
	
	public override void _Ready()
	{
		Player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		_label = GetNode<Godot.Label>("Label");
	}
	public override void _Process(double delta)
	{
		
		_points += IncomePerSecond * (float)delta;
		_label.Text = $"Points: {(int)_points}";
		TrySpawnShip();
		

	}

	private void TrySpawnShip()
	{
		if (_points < SpawnCost)
			return;

		_points -= SpawnCost;

		SpawnShip();
	}

	private void SpawnShip()
	{
		var ship = ShipPrefab.Instantiate<Node2D>();
		ship.GlobalPosition = GetSpawnPosition();
		GD.Print(ship.Position);
		AddChild(ship);
		
		SetupShip(ship);
	}

	private Vector2 GetSpawnPosition()
{
	
	
		int minRadius = 20;
		int maxRadius = 100;
		var rng = new RandomNumberGenerator();
		rng.Randomize();
		float angle = rng.RandiRange(0, 360);
		float radius = rng.RandiRange(minRadius, maxRadius);

		Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
		return offset;

	}
	private void SetupShip(Node2D ship)
	{
		Vector2 toPlayer = Player.GlobalPosition - ship.GlobalPosition;

		float baseAngle = toPlayer.Angle();
		var rng = new RandomNumberGenerator();
		rng.Randomize();
		
		float randomOffset = Mathf.DegToRad(rng.RandiRange(-20, 20));

		ship.Rotation = baseAngle + randomOffset;



	}



}
