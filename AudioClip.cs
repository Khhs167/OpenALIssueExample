using MP3Sharp;
using OpenTK.Audio.OpenAL;
using System.Buffers;
using System.Collections;
using System.Text;

namespace Soul.Engine.Audio;

public abstract class AudioClip : Resource
{
	public virtual void StreamInto(AudioSource source)
	{
		
	}
	public abstract void InitializeSource(AudioSource source);
}

public class FullAudioClip : AudioClip
{
	private int handle = -1;

	protected override void Load(byte[] data, string path)
	{
		Console.WriteLine("Loading full audio data!");
		handle = AL.GenBuffer();
		
		MemoryStream dataStream = new MemoryStream(data);
		MP3Stream mp3Stream = new MP3Stream(dataStream);
		MemoryStream memoryStream = new MemoryStream();
		mp3Stream.CopyTo(memoryStream);

		byte[] pcm = memoryStream.ToArray();

		Console.WriteLine($"Audio information: OpenAL Handle: {handle}, Format: {mp3Stream.Format}, Frequency: {mp3Stream.Frequency}");

		AL.BufferData(handle, mp3Stream.Format == SoundFormat.Pcm16BitMono ? ALFormat.Mono16 : ALFormat.Stereo16, pcm, mp3Stream.Frequency);
		AudioContext.GetError("LoadAudioClipFull");
	}
	public override void InitializeSource(AudioSource source)
	{
		Console.WriteLine("Loading audio buffer into source...");
		AL.Source(source.GetHandle(), ALSourcei.Buffer, handle);
		AudioContext.GetError("AudioStreamFull");
	}
}
