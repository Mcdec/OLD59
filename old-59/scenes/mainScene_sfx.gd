@tool
extends Node
class_name SFX

@export var SFX_IN_EDITOR := false


var random_sounds : Dictionary[Timer,AudioStreamPlayer3D]
@export var random_sounds_names : Dictionary[String,float] = { #file_name, play interval
	"lighting":20,
	"foghorn":30
}

func _ready() -> void:
	if SFX_IN_EDITOR or not Engine.is_editor_hint():
		for s in get_children():
			if s.is_class("AudioStreamPlayer"):
				s.play()
		random_sounds_init()

#-> Dictionary[Timer,AudioStreamPlayer3D]
func random_sounds_init() -> void :
	for sound in random_sounds_names:
		var stream_player = add_sound_player(sound)
		var time_interval : float = random_sounds_names.get(sound)
		var timer : Timer
		#Timer init
		for node in get_children():
			if node.name == sound.capitalize()+" Timer":
				timer = node
				break
		if timer == null:
			timer = RandomSoundTimer.new()
			add_child(timer)
			timer.owner = get_tree().edited_scene_root
			timer.name = sound.capitalize()+" Timer"
		timer.stream_player = stream_player
		timer.time_interval = time_interval
		timer.start(time_interval)

func add_sound_player(file_name:String) -> AudioStreamPlayer3D:
	##Places AudioStreamPlayer under parent node with a randomized stream by file_name sounds.
	var sound_player : AudioStreamPlayer3D
	for child in get_children():
		if child.name == file_name.capitalize():
			sound_player = child
			sound_player.stream = get_sounds(file_name)
			return sound_player
	sound_player = AudioStreamPlayer3D.new()
	sound_player.stream = get_sounds(file_name)
	sound_player.max_polyphony = 5
	add_child(sound_player)
	sound_player.owner = get_tree().edited_scene_root
	sound_player.name = file_name.capitalize()
	return sound_player

static func get_sounds(file_name:String) -> AudioStreamRandomizer:
	var sound_path := "res://asset/sounds/"
	var sound_folder : Array = Array(DirAccess.get_files_at(sound_path))
	var stream := AudioStreamRandomizer.new()
	stream.random_pitch = 0.7
	stream.random_volume_offset_db = 4
	var i := 0 #picked file index
	for s:String in sound_folder:
		if s.begins_with(file_name) and s.ends_with(".mp3"):
			var file = FileAccess.open(sound_path+s, FileAccess.READ)
			var sound = AudioStreamMP3.new()
			sound.data = file.get_buffer(file.get_length())
			stream.streams_count = i+1
			stream.set_stream(i,sound)
			i += 1
	
	if stream.streams_count == 0:
		push_warning("There're no '",file_name,"' sound")
	else:
		print("Successfully added ",stream.streams_count," sounds into ",file_name," playlist")
	return stream

static func play_spatial_sound(sound_player:AudioStreamPlayer3D,radius:float,pos:Vector2=Vector2(0,0)) -> void:
	print("Played ",sound_player.name)
	var twodpos := pos + Vector2(radius,0.0).rotated(randf()*360)
	sound_player.position.x = twodpos.x
	sound_player.position.z = twodpos.y
	sound_player.play()
