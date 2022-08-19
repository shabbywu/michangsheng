using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02000D6F RID: 3439
	internal class LinkedListIndex<TKey, TValue>
	{
		// Token: 0x0600615C RID: 24924 RVA: 0x00273481 File Offset: 0x00271681
		public LinkedListIndex(LinkedList<TValue> linkedList)
		{
			this.m_LinkedList = linkedList;
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x00273490 File Offset: 0x00271690
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

		// Token: 0x0600615E RID: 24926 RVA: 0x002734BC File Offset: 0x002716BC
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

		// Token: 0x0600615F RID: 24927 RVA: 0x002734F8 File Offset: 0x002716F8
		public void Add(TKey key, TValue value)
		{
			LinkedListNode<TValue> value2 = this.m_LinkedList.AddLast(value);
			if (this.m_Map == null)
			{
				this.m_Map = new Dictionary<TKey, LinkedListNode<TValue>>();
			}
			this.m_Map.Add(key, value2);
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x00273534 File Offset: 0x00271734
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

		// Token: 0x06006161 RID: 24929 RVA: 0x00273566 File Offset: 0x00271766
		public bool ContainsKey(TKey key)
		{
			return this.m_Map != null && this.m_Map.ContainsKey(key);
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x0027357E File Offset: 0x0027177E
		public void Clear()
		{
			if (this.m_Map != null)
			{
				this.m_Map.Clear();
			}
		}

		// Token: 0x04005585 RID: 21893
		private LinkedList<TValue> m_LinkedList;

		// Token: 0x04005586 RID: 21894
		private Dictionary<TKey, LinkedListNode<TValue>> m_Map;
	}
}
