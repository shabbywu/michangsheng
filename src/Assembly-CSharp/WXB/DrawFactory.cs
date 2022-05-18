using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000995 RID: 2453
	public static class DrawFactory
	{
		// Token: 0x06003EAC RID: 16044 RVA: 0x001B7F18 File Offset: 0x001B6118
		static DrawFactory()
		{
			DrawFactory.factorys.Add(DrawType.Outline, new DrawFactory.Factory<OutlineDraw>());
			DrawFactory.factorys.Add(DrawType.Alpha, new DrawFactory.Factory<AlphaDraw>());
			DrawFactory.factorys.Add(DrawType.Offset, new DrawFactory.Factory<OffsetDraw>());
			DrawFactory.factorys.Add(DrawType.OffsetAndAlpha, new DrawFactory.Factory<AlphaOffsetDraw>());
			DrawFactory.factorys.Add(DrawType.Cartoon, new DrawFactory.Factory<CartoonDraw>());
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x001B7F90 File Offset: 0x001B6190
		private static DrawFactory.IFactory Get(DrawType type)
		{
			if (type == DrawType.Default)
			{
				return DrawFactory.DefaultFactory;
			}
			DrawFactory.IFactory result = null;
			if (DrawFactory.factorys.TryGetValue(type, out result))
			{
				return result;
			}
			Debug.LogErrorFormat("type:{0} not find!", new object[]
			{
				type
			});
			return null;
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x001B7FD4 File Offset: 0x001B61D4
		public static Draw Create(GameObject go, DrawType type)
		{
			DrawFactory.IFactory factory = DrawFactory.Get(type);
			if (factory == null)
			{
				return null;
			}
			return factory.Create(go);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x001B7FF4 File Offset: 0x001B61F4
		public static void Free(Draw go)
		{
			if (go == null)
			{
				return;
			}
			DrawFactory.IFactory factory = DrawFactory.Get(go.type);
			if (factory == null)
			{
				go.DestroySelf();
				return;
			}
			factory.Free(go);
		}

		// Token: 0x0400388B RID: 14475
		private static int s_total = 0;

		// Token: 0x0400388C RID: 14476
		private static DrawFactory.IFactory DefaultFactory = new DrawFactory.Factory<DrawObject>();

		// Token: 0x0400388D RID: 14477
		private static Dictionary<DrawType, DrawFactory.IFactory> factorys = new Dictionary<DrawType, DrawFactory.IFactory>();

		// Token: 0x02000996 RID: 2454
		public interface IFactory
		{
			// Token: 0x06003EB0 RID: 16048
			Draw Create(GameObject go);

			// Token: 0x06003EB1 RID: 16049
			void Free(Draw d);
		}

		// Token: 0x02000997 RID: 2455
		public static class Pool<T> where T : MonoBehaviour, Draw, new()
		{
			// Token: 0x06003EB2 RID: 16050 RVA: 0x001B8024 File Offset: 0x001B6224
			public static T GetOrCreate(GameObject parent)
			{
				T t = default(T);
				while (DrawFactory.Pool<T>.EmptyList.Count != 0)
				{
					t = DrawFactory.Pool<T>.EmptyList.pop_back<T>();
					if (t != null)
					{
						break;
					}
				}
				if (t == null)
				{
					int num = ++DrawFactory.s_total;
					string name = num.ToString();
					t = parent.AddChild<T>();
					t.name = name;
				}
				t.transform.SetParent(parent.transform);
				t.OnInit();
				return t;
			}

			// Token: 0x06003EB3 RID: 16051 RVA: 0x0002D1C5 File Offset: 0x0002B3C5
			public static void Free(T d)
			{
				d.Release();
				DrawFactory.Pool<T>.UsedList.Remove(d);
				if (DrawFactory.Pool<T>.EmptyList.Count <= 10)
				{
					DrawFactory.Pool<T>.EmptyList.Add(d);
					return;
				}
				d.DestroySelf();
			}

			// Token: 0x0400388E RID: 14478
			public static List<T> EmptyList = new List<T>();

			// Token: 0x0400388F RID: 14479
			public static List<T> UsedList = new List<T>();
		}

		// Token: 0x02000998 RID: 2456
		public class Factory<T> : DrawFactory.IFactory where T : MonoBehaviour, Draw, new()
		{
			// Token: 0x06003EB5 RID: 16053 RVA: 0x0002D219 File Offset: 0x0002B419
			public Draw Create(GameObject go)
			{
				return DrawFactory.Pool<T>.GetOrCreate(go);
			}

			// Token: 0x06003EB6 RID: 16054 RVA: 0x0002D226 File Offset: 0x0002B426
			public void Free(Draw d)
			{
				DrawFactory.Pool<T>.Free((T)((object)d));
			}
		}
	}
}
