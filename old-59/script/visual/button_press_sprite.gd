@tool
extends Sprite2D

@onready var sprite_pushed: Sprite2D = $"../sprite_pushed"
@onready var button: Button = $".."

#@export var

func _ready() -> void:
	button.button_down.connect(button_down)
	button.button_up.connect(button_up)

func button_down() -> void:
	visible = false
	sprite_pushed.visible = true

func button_up() -> void:
	visible = true
	sprite_pushed.visible = false
