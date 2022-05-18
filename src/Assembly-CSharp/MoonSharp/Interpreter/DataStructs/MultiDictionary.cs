using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02001186 RID: 4486
	internal class MultiDictionary<K, V>
	{
		// Token: 0x06006D57 RID: 27991 RVA: 0x0004A81B File Offset: 0x00048A1B
		public MultiDictionary()
		{
			this.m_Map = new Dictionary<K, List<V>>();
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x0004A83A File Offset: 0x00048A3A
		public MultiDictionary(IEqualityComparer<K> eqComparer)
		{
			this.m_Map = new Dictionary<K, List<V>>(eqComparer);
		}

		// Token: 0x06006D59 RID: 27993 RVA: 0x00299FA0 File Offset: 0x002981A0
		public bool Add(K key, V value)
		{
			List<V> list;
			if (this.m_Map.TryGetValue(key, out list))
			{
				list.Add(value);
				return false;
			}
			list = new List<V>();
			list.Add(value);
			this.m_Map.Add(key, list);
			return true;
		}

		// Token: 0x06006D5A RID: 27994 RVA: 0x00299FE4 File Offset: 0x002981E4
		public IEnumerable<V> Find(K key)
		{
			List<V> result;
			if (this.m_Map.TryGetValue(key, out result))
			{
				return result;
			}
			return this.m_DefaultRet;
		}

		// Token: 0x06006D5B RID: 27995 RVA: 0x0004A85A File Offset: 0x00048A5A
		public bool ContainsKey(K key)
		{
			return this.m_Map.ContainsKey(key);
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06006D5C RID: 27996 RVA: 0x0004A868 File Offset: 0x00048A68
		public IEnumerable<K> Keys
		{
			get
			{
				return this.m_Map.Keys;
			}
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x0004A875 File Offset: 0x00048A75
		public void Clear()
		{
			this.m_Map.Clear();
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x0004A882 File Offset: 0x00048A82
		public void Remove(K key)
		{
			this.m_Map.Remove(key);
		}

		// Token: 0x06006D5F RID: 27999 RVA: 0x0029A00C File Offset: 0x0029820C
		public bool RemoveValue(K key, V value)
		{
			List<V> list;
			if (this.m_Map.TryGetValue(key, out list))
			{
				list.Remove(value);
				if (list.Count == 0)
				{
					this.Remove(key);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04006222 RID: 25122
		private Dictionary<K, List<V>> m_Map;

		// Token: 0x04006223 RID: 25123
		private V[] m_DefaultRet = new V[0];
	}
}
