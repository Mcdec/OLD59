@tool
extends Sprite2D

@onready var sprite_pushed: Sprite2D = $"../sprite_pushed"
@onready var button: Button = $".."

#@export var

var timer := Timer.new()

func _ready() -> void:
	button.pressed.connect(button_down)
	timer.one_shot = true
	timer.timeout.connect(button_up)
	button.add_child.call_deferred(timer)

func button_down() -> void:
	visible = false
	sprite_pushed.visible = true
	timer.start(0.35)

func button_up() -> void:
	visible = true
	sprite_pushed.visible = false
