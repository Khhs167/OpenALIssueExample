using Soul.Engine.Audio;
using Soul.Engine;

Console.WriteLine("Audio test");
Console.WriteLine("Current Working directory: " + Directory.GetCurrentDirectory());
Console.WriteLine("Please put an mp3 in the same directory as the working directory");
AudioContext.Initialize();
AudioSource source = new AudioSource();
Console.Write("Relative path to sound file(mp3 only): ");
string path = Console.ReadLine()!;
AudioClip clip = Resource.Load<FullAudioClip>(path);
source.Clip = clip;
source.Play();

while(source.Playing) ;