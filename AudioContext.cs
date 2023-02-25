using OpenTK.Audio.OpenAL;

namespace Soul.Engine.Audio;

public static unsafe class AudioContext
{
	private static ALContext context;
	public static void Initialize()
	{
		Console.WriteLine("Setting up OpenAL!");
		ALDevice alDevice = ALC.OpenDevice(null);
		Console.WriteLine("Selected device: " + alDevice.Handle);
		context = ALC.CreateContext(alDevice, (int*)null);
		Console.WriteLine("Created context " + context.Handle);
		ALC.MakeContextCurrent(context);
		ALC.ProcessContext(context);
		ALBase.RegisterOpenALResolver();

		GetError("ContextCreation");
	}

	public static void GetError(string? location = null)
	{
		ALError error = AL.GetError();
		if (error != ALError.NoError)
		{
			if(location == null)
				throw new Exception("AL ERROR: " + error);
			throw new Exception("AL ERROR[" + location +"]: " + error);
		}
	}
}
