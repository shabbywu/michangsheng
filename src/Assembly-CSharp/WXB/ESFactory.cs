using System.Collections.Generic;

namespace WXB;

public class ESFactory
{
	private static Dictionary<string, ElementSegment> TypeList;

	static ESFactory()
	{
		TypeList = new Dictionary<string, ElementSegment>();
		TypeList.Add("Default", new DefaultES());
	}

	public static void Add(string name, ElementSegment es)
	{
		TypeList.Add(name, es);
	}

	public static bool Remove(string name)
	{
		return TypeList.Remove(name);
	}

	public static ElementSegment Get(string name)
	{
		ElementSegment value = null;
		if (TypeList.TryGetValue(name, out value))
		{
			return value;
		}
		return null;
	}

	public static List<string> GetAllName()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, ElementSegment> type in TypeList)
		{
			list.Add(type.Key);
		}
		return list;
	}
}
