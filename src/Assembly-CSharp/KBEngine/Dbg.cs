using System;
using UnityEngine;

namespace KBEngine;

public class Dbg
{
	public static DEBUGLEVEL debugLevel;

	public static string getHead()
	{
		return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "] ";
	}

	public static void INFO_MSG(object s)
	{
		if (DEBUGLEVEL.INFO >= debugLevel)
		{
			Debug.Log((object)(getHead() + s));
		}
	}

	public static void DEBUG_MSG(object s)
	{
		if (DEBUGLEVEL.DEBUG >= debugLevel)
		{
			Debug.Log((object)(getHead() + s));
		}
	}

	public static void WARNING_MSG(object s)
	{
		if (DEBUGLEVEL.WARNING >= debugLevel)
		{
			Debug.LogWarning((object)(getHead() + s));
		}
	}

	public static void ERROR_MSG(object s)
	{
		if (DEBUGLEVEL.ERROR >= debugLevel)
		{
			Debug.LogError((object)(getHead() + s));
		}
	}

	public static void profileStart(string name)
	{
	}

	public static void profileEnd(string name)
	{
	}
}
