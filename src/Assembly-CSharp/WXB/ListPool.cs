using System.Collections.Generic;
using UnityEngine.Events;

namespace WXB;

internal static class ListPool<T>
{
	private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, (UnityAction<List<T>>)(object)(UnityAction<List<List<T>>>)delegate(List<T> l)
	{
		l.Clear();
	});

	public static List<T> Get()
	{
		return s_ListPool.Get();
	}

	public static void Release(List<T> toRelease)
	{
		s_ListPool.Release(toRelease);
	}
}
