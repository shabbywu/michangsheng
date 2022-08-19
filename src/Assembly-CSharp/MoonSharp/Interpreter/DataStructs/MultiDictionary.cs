using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02000D70 RID: 3440
	internal class MultiDictionary<K, V>
	{
		// Token: 0x06006163 RID: 24931 RVA: 0x00273593 File Offset: 0x00271793
		public MultiDictionary()
		{
			this.m_Map = new Dictionary<K, List<V>>();
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x002735B2 File Offset: 0x002717B2
		public MultiDictionary(IEqualityComparer<K> eqComparer)
		{
			this.m_Map = new Dictionary<K, List<V>>(eqComparer);
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x002735D4 File Offset: 0x002717D4
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

		// Token: 0x06006166 RID: 24934 RVA: 0x00273618 File Offset: 0x00271818
		public IEnumerable<V> Find(K key)
		{
			List<V> result;
			if (this.m_Map.TryGetValue(key, out result))
			{
				return result;
			}
			return this.m_DefaultRet;
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x0027363D File Offset: 0x0027183D
		public bool ContainsKey(K key)
		{
			return this.m_Map.ContainsKey(key);
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06006168 RID: 24936 RVA: 0x0027364B File Offset: 0x0027184B
		public IEnumerable<K> Keys
		{
			get
			{
				return this.m_Map.Keys;
			}
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x00273658 File Offset: 0x00271858
		public void Clear()
		{
			this.m_Map.Clear();
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x00273665 File Offset: 0x00271865
		public void Remove(K key)
		{
			this.m_Map.Remove(key);
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x00273674 File Offset: 0x00271874
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

		// Token: 0x04005587 RID: 21895
		private Dictionary<K, List<V>> m_Map;

		// Token: 0x04005588 RID: 21896
		private V[] m_DefaultRet = new V[0];
	}
}
