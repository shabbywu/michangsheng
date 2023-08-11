using System.Collections.Generic;

namespace KBEngine;

public class ObjectPool<T> where T : new()
{
	private static Stack<T> _objects = new Stack<T>();

	private static T v;

	public static T createObject()
	{
		lock (_objects)
		{
			if (_objects.Count > 0)
			{
				v = _objects.Pop();
				return v;
			}
			return new T();
		}
	}

	public static void reclaimObject(T item)
	{
		lock (_objects)
		{
			_objects.Push(item);
		}
	}
}
