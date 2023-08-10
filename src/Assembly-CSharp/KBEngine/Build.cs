namespace KBEngine;

public class Build : BuildBase
{
	public override void __init__()
	{
	}

	public override object getDefinedProperty(string name)
	{
		if (name == "BuildId")
		{
			return BuildId;
		}
		return null;
	}
}
