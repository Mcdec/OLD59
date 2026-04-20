using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class SpawnManager : Node
{
	public float SpawnCost = 15f;
	
	private Node2D Player;
	private float points;
	Godot.Timer _timer = new Godot.Timer();
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
		

		
		InputRotation = i;
		
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
	
	
		int minRadius = 70;
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
	
	AddChild(ship);
	
	
}

}
