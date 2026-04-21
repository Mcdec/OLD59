extends Timer
class_name RandomSoundTimer

var stream_player = AudioStreamPlayer3D
var time_interval : float

func _ready() -> void:
	timeout.connect(on_timeout)

func on_timeout() -> void:
	SFX.play_spatial_sound(stream_player,30)
	start(time_interval * (randf() + 0.5))
