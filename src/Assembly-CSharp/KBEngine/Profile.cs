using System;

namespace KBEngine;

public class Profile
{
	private DateTime startTime;

	private string _name = "";

	public Profile(string name)
	{
		_name = name;
	}

	~Profile()
	{
	}

	public void start()
	{
		startTime = DateTime.Now;
	}

	public void end()
	{
		TimeSpan timeSpan = DateTime.Now - startTime;
		if (timeSpan.TotalMilliseconds >= 100.0)
		{
			Dbg.WARNING_MSG("Profile::profile(): '" + _name + "' took " + timeSpan.TotalMilliseconds + " ms");
		}
	}
}
