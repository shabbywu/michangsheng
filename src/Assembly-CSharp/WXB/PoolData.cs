using System;
using System.Collections.Generic;

namespace WXB;

internal class PoolData<T> where T : new()
{
	public static List<T> bufs = new List<T>();

	public static T Get()
	{
		if (bufs.Count == 0)
		{
			return new T();
		}
		T result = bufs[bufs.Count - 1];
		bufs.RemoveAt(bufs.Count - 1);
		return result;
	}

	public static void Free(T t)
	{
		bufs.Add(t);
	}

	public static void FreeList(List<T> list, Action<T> fun)
	{
		for (int i = 0; i < list.Count; i++)
		{
			fun(list[i]);
		}
		bufs.AddRange(list);
		list.Clear();
	}
}
