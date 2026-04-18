extends Button

@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D

var wasHovered : bool = false
var animFrame : int = 0

func _ready() -> void:
	animated_sprite_2d.play("deselect")
	#animated_sprite_2d.frame = 12

 
func _process(_delta: float) -> void:
	if is_hovered() > wasHovered:
		print("hovered")
		animFrame = animated_sprite_2d.frame
		animated_sprite_2d.play("select")
		#animated_sprite_2d.frame = animFrame
	if is_hovered() < wasHovered:
		print("over")
		animFrame = animated_sprite_2d.frame
		animated_sprite_2d.play("deselect")
		#animated_sprite_2d.frame = animFrame
		wasHovered = is_hovered()
	
#
#func _pressed() -> void:
	#animated_sprite_2d.play("press")
