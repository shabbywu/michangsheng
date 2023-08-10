using System;

[AttributeUsage(AttributeTargets.Class)]
public class BindPrefab : Attribute
{
	public string Path { get; private set; }

	public BindPrefab(string path)
	{
		Path = path;
	}
}
