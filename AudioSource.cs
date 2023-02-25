using OpenTK.Audio.OpenAL;

namespace Soul.Engine.Audio;

public class AudioSource
{
	private readonly int handle;
	private AudioClip? clip;

	public AudioClip? Clip
	{
		get
		{
			return clip;
		}
		set
		{
			Stop();
			value?.InitializeSource(this);

			clip = value;
		}
	}

	public bool Playing
	{
		get
		{
			ALSourceState state = AL.GetSourceState(handle);
			return state == ALSourceState.Playing;
		}
	}
	
	public bool Paused
	{
		get
		{
			ALSourceState state = AL.GetSourceState(handle);
			return state == ALSourceState.Paused;
		}
		set
		{
			if(value)
				AL.SourcePause(handle);
		}
	}
	
	public AudioSource()
	{
		Console.WriteLine("Creating non-looping, non-relative source");
		handle = AL.GenSource();
		AL.Source(handle, ALSourceb.Looping, true);
		AL.Source(handle, ALSourceb.SourceRelative, false);
	}

	public void Stop()
	{
		AL.SourceStop(handle);
	}

	public void Play()
	{
		AL.SourcePlay(handle);
	}

	public void Continue()
	{
		if(clip != null)
			clip.StreamInto(this);
	}

	public int GetHandle()
	{
		return handle;
	}
}
