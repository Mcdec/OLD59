using Godot;
using System;
using System.ComponentModel;

public partial class RadarCircle : Node2D
{
	[Export] public PackedScene RadarCirclePrefab;
	public override void _Ready()
	{
		
		AddToGroup("RadarCircle");
	}
	void RadarStart()
	{
		var radar = RadarCirclePrefab.Instantiate<Sprite2D>();
		radar.Position = Position;
		AddChild(radar);
	}
	
}
