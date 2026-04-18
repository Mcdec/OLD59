using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class SpawnManager : Node
{
	public float SpawnCost = 1000f;
	private Node2D Player;
	private float points;
	[Export]public PackedScene ShipPrefab;
	float input = 0;
	float angle = 0;
	public float InputRotation
	{
		get
		{
			return input;
		}
		set
		{
			input = value;

			GetTree().CallGroup("ship", "updaterotation", value);
		}
	}
	public override void _Ready()
	{
		
		
		Player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		AddToGroup("points");
		AddToGroup("angle");
	}
	private void updateangle(float i)
	{
		

		
		InputRotation += i;
		GD.Print("1 "+InputRotation);
	}
	private void updatepoints(float i)
	{
		
		points = i;
		TrySpawnShip();
	}
	
	private void TrySpawnShip()
{
	if (points < SpawnCost)
		return;
		
		GetNode<GameManager>("/root/_gameManager").Points -= SpawnCost;

	SpawnShip();
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
	private void SpawnShip()
{
	var ship = ShipPrefab.Instantiate<Node2D>();
	ship.GlobalPosition = GetSpawnPosition();
	GD.Print(ship.Position);
	AddChild(ship);
	
	SetupShip(ship);
}
	private void SetupShip(Node2D ship)
{
	Vector2 toPlayer = Player.GlobalPosition - ship.GlobalPosition;

	float baseAngle = toPlayer.Angle();
	var rng = new RandomNumberGenerator();
	rng.Randomize();
	
	float randomOffset = Mathf.DegToRad(rng.RandiRange(-20, 20));

		InputRotation += randomOffset;
		
}
}
