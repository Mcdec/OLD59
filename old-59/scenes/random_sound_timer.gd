extends Timer
class_name RandomSoundTimer

var stream_player = AudioStreamPlayer3D
var time_interval : float
var position : Vector2 = Vector2(0,0)
var radius : float = 0

func _ready() -> void:
	timeout.connect(on_timeout)

func on_timeout() -> void:
	SFX.play_spatial_sound(stream_player,radius,position)
	start(time_interval * (randf() + 0.5))
