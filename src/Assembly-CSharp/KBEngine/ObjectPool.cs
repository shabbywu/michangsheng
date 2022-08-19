using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C54 RID: 3156
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x060055BE RID: 21950 RVA: 0x0023A020 File Offset: 0x00238220
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

		// Token: 0x060055BF RID: 21951 RVA: 0x0023A084 File Offset: 0x00238284
		public static void reclaimObject(T item)
		{
			Stack<T> objects = ObjectPool<T>._objects;
			lock (objects)
			{
				ObjectPool<T>._objects.Push(item);
			}
		}

		// Token: 0x040050C4 RID: 20676
		private static Stack<T> _objects = new Stack<T>();

		// Token: 0x040050C5 RID: 20677
		private static T v;
	}
}
