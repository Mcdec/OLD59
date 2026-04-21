@tool
extends Sprite2D

@onready var sprite_pushed: Sprite2D = $"../sprite_pushed"
@onready var button: Button = $".."
@onready var visual: Visual = $"../.."
@onready var sfx: SFX = $"../../SFX"


var sp_button : AudioStreamPlayer3D

var timer := Timer.new()

func _ready() -> void:
	button.pressed.connect(button_down)
	timer.one_shot = true
	timer.timeout.connect(button_up)
	button.add_child.call_deferred(timer)
	sp_button = sfx.add_sound_player("button")

func button_down() -> void:
	visible = false
	sprite_pushed.visible = true
	timer.start(0.35)
	SFX.play_spatial_sound(sp_button,0)

func button_up() -> void:
	visible = true
	sprite_pushed.visible = false
