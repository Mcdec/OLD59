@tool
extends Control
class_name Visual

@onready var light: Sprite2D = $light
@onready var radar_glass: Sprite2D = $radar_glass
@onready var foreground: Sprite2D = $foreground
@onready var window: Sprite2D = $window
@onready var background: Sprite2D = $background

@export var LIGHTNING_INTERVAL : float = 12


var sp_lightning := add_sound_player(self,"lightning",1)
var sounds_lightning := get_sounds("lighting")

var lightning_interval := LIGHTNING_INTERVAL
var lightning_timer : Timer = Timer.new()



func _ready() -> void:
	lightning_timer.timeout.connect(lightning_strike)
	add_child(lightning_timer)
	#lightning_timer.start(lightning_interval)
	lightning_timer.wait_time = lightning_interval

#func _physics_process(delta: float) -> void:

func lightning_strike() -> void:
	lightning_interval = LIGHTNING_INTERVAL * (randf() + 0.5)
	lightning_timer.wait_time = lightning_interval
	play_spatial_sound(sp_lightning,sounds_lightning,400)
	print('lightning strike')

static func add_sound_player(parent:Node,sp_name:String,polyphony:int=3) -> AudioStreamPlayer2D:
	var sound_player = AudioStreamPlayer2D.new()
	parent.add_child(sound_player)
	sound_player.max_polyphony = polyphony
	sound_player.name = sp_name
	return sound_player

static func play_spatial_sound(sound_player:AudioStreamPlayer2D,sounds:Array[AudioStreamMP3],radius:float,pos:Vector2=Vector2(0,0)) -> void:
	var sound : AudioStreamMP3 = sounds.pick_random()
	sound_player.stream = sound
	sound_player.position = pos + Vector2(radius,0.0).rotated(randf()*360)
	sound_player.play()

static func get_sounds(file_name:String) -> Array[AudioStreamMP3]:
	var sound_path := "res://asset/sounds/"
	var sound_folder : Array = Array(DirAccess.get_files_at(sound_path))
	var array : Array[AudioStreamMP3]
	for s in sound_folder:
		if s.begins_with(file_name):
			var file = FileAccess.open(sound_path+file_name+".mp3", FileAccess.READ)
			var sound = AudioStreamMP3.new()
			sound.data = file.get_buffer(file.get_length())
			array.push_front(sound)
	if array.is_empty():
		push_warning("There're no '",file_name,"' sound")
	return array

	
	
	
	
