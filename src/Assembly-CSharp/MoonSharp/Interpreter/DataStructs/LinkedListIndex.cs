using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02001185 RID: 4485
	internal class LinkedListIndex<TKey, TValue>
	{
		// Token: 0x06006D50 RID: 27984 RVA: 0x0004A7DF File Offset: 0x000489DF
		public LinkedListIndex(LinkedList<TValue> linkedList)
		{
			this.m_LinkedList = linkedList;
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x00299EC8 File Offset: 0x002980C8
		public LinkedListNode<TValue> Find(TKey key)
		{
			if (this.m_Map == null)
			{
				return null;
			}
			LinkedListNode<TValue> result;
			if (this.m_Map.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x00299EF4 File Offset: 0x002980F4
		public TValue Set(TKey key, TValue value)
		{
			LinkedListNode<TValue> linkedListNode = this.Find(key);
			if (linkedListNode == null)
			{
				this.Add(key, value);
				return default(TValue);
			}
			TValue value2 = linkedListNode.Value;
			linkedListNode.Value = value;
			return value2;
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x00299F30 File Offset: 0x00298130
		public void Add(TKey key, TValue value)
		{
			LinkedListNode<TValue> value2 = this.m_LinkedList.AddLast(value);
			if (this.m_Map == null)
			{
				this.m_Map = new Dictionary<TKey, LinkedListNode<TValue>>();
			}
			this.m_Map.Add(key, value2);
		}

		// Token: 0x06006D54 RID: 27988 RVA: 0x00299F6C File Offset: 0x0029816C
		public bool Remove(TKey key)
		{
			LinkedListNode<TValue> linkedListNode = this.Find(key);
			if (linkedListNode != null)
			{
				this.m_LinkedList.Remove(linkedListNode);
				return this.m_Map.Remove(key);
			}
			return false;
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x0004A7EE File Offset: 0x000489EE
		public bool ContainsKey(TKey key)
		{
			return this.m_Map != null && this.m_Map.ContainsKey(key);
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x0004A806 File Offset: 0x00048A06
		public void Clear()
		{
			if (this.m_Map != null)
			{
				this.m_Map.Clear();
			}
		}

		// Token: 0x04006220 RID: 25120
		private LinkedList<TValue> m_LinkedList;

		// Token: 0x04006221 RID: 25121
		private Dictionary<TKey, LinkedListNode<TValue>> m_Map;
	}
}
