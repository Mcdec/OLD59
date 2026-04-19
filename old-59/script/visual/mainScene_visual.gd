@tool
extends Control

@onready var light: Sprite2D = $light
@onready var radar_glass: Sprite2D = $radar_glass
@onready var foreground: Sprite2D = $foreground
@onready var window: Sprite2D = $window
@onready var background: Sprite2D = $background

@export_category("Flicker")
@export var LIGHTING_INTERVAL : float = 12

var lighting_interval := LIGHTING_INTERVAL
var lighting_timer : Timer = Timer.new()



func _ready() -> void:
	lighting_timer.timeout.connect(lightning_strike)
	lighting_timer.autostart = true
	lighting_timer.wait_time = lighting_interval

func _process(delta: float) -> void:
	pass

func lightning_strike() -> void:
	lighting_interval = LIGHTING_INTERVAL * (randf() + 0.5)
	lighting_timer.wait_time = lighting_interval
	emit_spatial_sound()


func emit_spatial_sound() -> void:
	pass
