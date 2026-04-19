@tool
extends Sprite2D

@onready var sprite_pushed: Sprite2D = $"../sprite_pushed"
@onready var button: Button = $".."
@onready var visual: Visual = $"../.."

var sp_button : AudioStreamPlayer2D
var sounds_button := Visual.get_sounds("button")

var timer := Timer.new()

func _ready() -> void:
	sp_button = Visual.add_sound_player(self,"button")
	button.pressed.connect(button_down)
	timer.one_shot = true
	timer.timeout.connect(button_up)
	button.add_child.call_deferred(timer)

func button_down() -> void:
	visible = false
	sprite_pushed.visible = true
	timer.start(0.35)
	Visual.play_spatial_sound(sp_button,sounds_button,0)

func button_up() -> void:
	visible = true
	sprite_pushed.visible = false
