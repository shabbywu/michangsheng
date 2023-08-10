using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public static class DrawFactory
{
	public interface IFactory
	{
		Draw Create(GameObject go);

		void Free(Draw d);
	}

	public static class Pool<T> where T : MonoBehaviour, Draw, new()
	{
		public static List<T> EmptyList = new List<T>();

		public static List<T> UsedList = new List<T>();

		public static T GetOrCreate(GameObject parent)
		{
			T val = default(T);
			while (EmptyList.Count != 0)
			{
				val = EmptyList.pop_back();
				if ((Object)(object)val != (Object)null)
				{
					break;
				}
			}
			if ((Object)(object)val == (Object)null)
			{
				int num = ++s_total;
				string name = num.ToString();
				val = parent.AddChild<T>();
				((Object)(object)val).name = name;
			}
			((Component)(object)val).transform.SetParent(parent.transform);
			val.OnInit();
			return val;
		}

		public static void Free(T d)
		{
			d.Release();
			UsedList.Remove(d);
			if (EmptyList.Count <= 10)
			{
				EmptyList.Add(d);
			}
			else
			{
				d.DestroySelf();
			}
		}
	}

	public class Factory<T> : IFactory where T : MonoBehaviour, Draw, new()
	{
		public Draw Create(GameObject go)
		{
			return Pool<T>.GetOrCreate(go);
		}

		public void Free(Draw d)
		{
			Pool<T>.Free((T)d);
		}
	}

	private static int s_total;

	private static IFactory DefaultFactory;

	private static Dictionary<DrawType, IFactory> factorys;

	static DrawFactory()
	{
		s_total = 0;
		DefaultFactory = new Factory<DrawObject>();
		factorys = new Dictionary<DrawType, IFactory>();
		factorys.Add(DrawType.Outline, new Factory<OutlineDraw>());
		factorys.Add(DrawType.Alpha, new Factory<AlphaDraw>());
		factorys.Add(DrawType.Offset, new Factory<OffsetDraw>());
		factorys.Add(DrawType.OffsetAndAlpha, new Factory<AlphaOffsetDraw>());
		factorys.Add(DrawType.Cartoon, new Factory<CartoonDraw>());
	}

	private static IFactory Get(DrawType type)
	{
		if (type == DrawType.Default)
		{
			return DefaultFactory;
		}
		IFactory value = null;
		if (factorys.TryGetValue(type, out value))
		{
			return value;
		}
		Debug.LogErrorFormat("type:{0} not find!", new object[1] { type });
		return null;
	}

	public static Draw Create(GameObject go, DrawType type)
	{
		return Get(type)?.Create(go);
	}

	public static void Free(Draw go)
	{
		if (go != null)
		{
			IFactory factory = Get(go.type);
			if (factory == null)
			{
				go.DestroySelf();
			}
			else
			{
				factory.Free(go);
			}
		}
	}
}
