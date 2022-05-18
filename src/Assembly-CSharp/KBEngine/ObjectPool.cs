using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FDC RID: 4060
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x06006005 RID: 24581 RVA: 0x00266EE4 File Offset: 0x002650E4
		public static T createObject()
		{
			Stack<T> objects = ObjectPool<T>._objects;
			T result;
			lock (objects)
			{
				if (ObjectPool<T>._objects.Count > 0)
				{
					ObjectPool<T>.v = ObjectPool<T>._objects.Pop();
					result = ObjectPool<T>.v;
				}
				else
				{
					result = Activator.CreateInstance<T>();
				}
			}
			return result;
		}

		// Token: 0x06006006 RID: 24582 RVA: 0x00266F48 File Offset: 0x00265148
		public static void reclaimObject(T item)
		{
			Stack<T> objects = ObjectPool<T>._objects;
			lock (objects)
			{
				ObjectPool<T>._objects.Push(item);
			}
		}

		// Token: 0x04005B74 RID: 23412
		private static Stack<T> _objects = new Stack<T>();

		// Token: 0x04005B75 RID: 23413
		private static T v;
	}
}
