using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000687 RID: 1671
	public static class DrawFactory
	{
		// Token: 0x060034FA RID: 13562 RVA: 0x0016FB9C File Offset: 0x0016DD9C
		static DrawFactory()
		{
			DrawFactory.factorys.Add(DrawType.Outline, new DrawFactory.Factory<OutlineDraw>());
			DrawFactory.factorys.Add(DrawType.Alpha, new DrawFactory.Factory<AlphaDraw>());
			DrawFactory.factorys.Add(DrawType.Offset, new DrawFactory.Factory<OffsetDraw>());
			DrawFactory.factorys.Add(DrawType.OffsetAndAlpha, new DrawFactory.Factory<AlphaOffsetDraw>());
			DrawFactory.factorys.Add(DrawType.Cartoon, new DrawFactory.Factory<CartoonDraw>());
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x0016FC14 File Offset: 0x0016DE14
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

		// Token: 0x060034FC RID: 13564 RVA: 0x0016FC58 File Offset: 0x0016DE58
		public static Draw Create(GameObject go, DrawType type)
		{
			DrawFactory.IFactory factory = DrawFactory.Get(type);
			if (factory == null)
			{
				return null;
			}
			return factory.Create(go);
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x0016FC78 File Offset: 0x0016DE78
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

		// Token: 0x04002EE1 RID: 12001
		private static int s_total = 0;

		// Token: 0x04002EE2 RID: 12002
		private static DrawFactory.IFactory DefaultFactory = new DrawFactory.Factory<DrawObject>();

		// Token: 0x04002EE3 RID: 12003
		private static Dictionary<DrawType, DrawFactory.IFactory> factorys = new Dictionary<DrawType, DrawFactory.IFactory>();

		// Token: 0x020014F8 RID: 5368
		public interface IFactory
		{
			// Token: 0x0600828E RID: 33422
			Draw Create(GameObject go);

			// Token: 0x0600828F RID: 33423
			void Free(Draw d);
		}

		// Token: 0x020014F9 RID: 5369
		public static class Pool<T> where T : MonoBehaviour, Draw, new()
		{
			// Token: 0x06008290 RID: 33424 RVA: 0x002DB530 File Offset: 0x002D9730
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

			// Token: 0x06008291 RID: 33425 RVA: 0x002DB5C5 File Offset: 0x002D97C5
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

			// Token: 0x04006DEC RID: 28140
			public static List<T> EmptyList = new List<T>();

			// Token: 0x04006DED RID: 28141
			public static List<T> UsedList = new List<T>();
		}

		// Token: 0x020014FA RID: 5370
		public class Factory<T> : DrawFactory.IFactory where T : MonoBehaviour, Draw, new()
		{
			// Token: 0x06008293 RID: 33427 RVA: 0x002DB619 File Offset: 0x002D9819
			public Draw Create(GameObject go)
			{
				return DrawFactory.Pool<T>.GetOrCreate(go);
			}

			// Token: 0x06008294 RID: 33428 RVA: 0x002DB626 File Offset: 0x002D9826
			public void Free(Draw d)
			{
				DrawFactory.Pool<T>.Free((T)((object)d));
			}
		}
	}
}
