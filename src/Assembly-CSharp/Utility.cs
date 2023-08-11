internal class Utility
{
	public static int getPostInt(string name)
	{
		return int.Parse(name.Substring(name.IndexOf('_') + 1));
	}

	public static string getPreString(string name)
	{
		return name.Substring(0, name.IndexOf('_'));
	}
}
