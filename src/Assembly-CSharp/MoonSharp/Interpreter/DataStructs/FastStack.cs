using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02000D6D RID: 3437
	internal class FastStack<T> : IList<T>, ICollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06006137 RID: 24887 RVA: 0x002731D7 File Offset: 0x002713D7
		public FastStack(int maxCapacity)
		{
			this.m_Storage = new T[maxCapacity];
		}

		// Token: 0x170007B5 RID: 1973
		public T this[int index]
		{
			get
			{
				return this.m_Storage[index];
			}
			set
			{
				this.m_Storage[index] = value;
			}
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x00273208 File Offset: 0x00271408
		public T Push(T item)
		{
			T[] storage = this.m_Storage;
			int headIdx = this.m_HeadIdx;
			this.m_HeadIdx = headIdx + 1;
			storage[headIdx] = item;
			return item;
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x00273233 File Offset: 0x00271433
		public void Expand(int size)
		{
			this.m_HeadIdx += size;
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x00273243 File Offset: 0x00271443
		private void Zero(int from, int to)
		{
			Array.Clear(this.m_Storage, from, to - from + 1);
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x00273258 File Offset: 0x00271458
		private void Zero(int index)
		{
			this.m_Storage[index] = default(T);
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x0027327A File Offset: 0x0027147A
		public T Peek(int idxofs = 0)
		{
			return this.m_Storage[this.m_HeadIdx - 1 - idxofs];
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x00273291 File Offset: 0x00271491
		public void Set(int idxofs, T item)
		{
			this.m_Storage[this.m_HeadIdx - 1 - idxofs] = item;
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x002732A9 File Offset: 0x002714A9
		public void CropAtCount(int p)
		{
			this.RemoveLast(this.Count - p);
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x002732BC File Offset: 0x002714BC
		public void RemoveLast(int cnt = 1)
		{
			if (cnt == 1)
			{
				this.m_HeadIdx--;
				this.m_Storage[this.m_HeadIdx] = default(T);
				return;
			}
			int headIdx = this.m_HeadIdx;
			this.m_HeadIdx -= cnt;
			this.Zero(this.m_HeadIdx, headIdx);
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x00273318 File Offset: 0x00271518
		public T Pop()
		{
			this.m_HeadIdx--;
			T result = this.m_Storage[this.m_HeadIdx];
			this.m_Storage[this.m_HeadIdx] = default(T);
			return result;
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0027335E File Offset: 0x0027155E
		public void Clear()
		{
			Array.Clear(this.m_Storage, 0, this.m_Storage.Length);
			this.m_HeadIdx = 0;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06006144 RID: 24900 RVA: 0x0027337B File Offset: 0x0027157B
		public int Count
		{
			get
			{
				return this.m_HeadIdx;
			}
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		int IList<!0>.IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170007B7 RID: 1975
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value;
			}
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x00273396 File Offset: 0x00271596
		void ICollection<!0>.Add(T item)
		{
			this.Push(item);
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x002733A0 File Offset: 0x002715A0
		void ICollection<!0>.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		bool ICollection<!0>.Contains(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600614E RID: 24910 RVA: 0x002733A8 File Offset: 0x002715A8
		int ICollection<!0>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600614F RID: 24911 RVA: 0x0000280F File Offset: 0x00000A0F
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005583 RID: 21891
		private T[] m_Storage;

		// Token: 0x04005584 RID: 21892
		private int m_HeadIdx;
	}
}
