namespace Soul.Engine;

/// <summary>
/// A basic class for resources. Used pretty much everywhere
/// </summary>
// ReSharper disable once InconsistentNaming
public abstract class Resource : Object
{

	private static readonly Dictionary<string, Resource> Resources = new Dictionary<string, Resource>();

	/// <summary>
	/// Load the resource from a set of bytes
	/// </summary>
	/// <param name="data"></param>
	protected abstract void Load(byte[] data, string path);

	/// <summary>
	/// Load in a resource from the content pipeline
	/// </summary>
	/// <param name="path">The path to the resource</param>
	/// <typeparam name="T">The type of the resource</typeparam>
	/// <returns>The resource. Hopefully</returns>
	public static T Load<T>(string path, bool cached = false) where T : Resource, new()
	{

		if (cached && Resources.ContainsKey(path))
			return (T)Resources[path];
		
		byte[] data = File.ReadAllBytes(path);
		T value = new T();
		value.Load(data, path);
		if(cached)
			Resources[path] = value;
		return value;
	}
}
