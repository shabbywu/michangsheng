using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02001183 RID: 4483
	internal class FastStack<T> : IList<T>, ICollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06006D2B RID: 27947 RVA: 0x0004A670 File Offset: 0x00048870
		public FastStack(int maxCapacity)
		{
			this.m_Storage = new T[maxCapacity];
		}

		// Token: 0x17000A12 RID: 2578
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

		// Token: 0x06006D2E RID: 27950 RVA: 0x00299D8C File Offset: 0x00297F8C
		public T Push(T item)
		{
			T[] storage = this.m_Storage;
			int headIdx = this.m_HeadIdx;
			this.m_HeadIdx = headIdx + 1;
			storage[headIdx] = item;
			return item;
		}

		// Token: 0x06006D2F RID: 27951 RVA: 0x0004A6A1 File Offset: 0x000488A1
		public void Expand(int size)
		{
			this.m_HeadIdx += size;
		}

		// Token: 0x06006D30 RID: 27952 RVA: 0x0004A6B1 File Offset: 0x000488B1
		private void Zero(int from, int to)
		{
			Array.Clear(this.m_Storage, from, to - from + 1);
		}

		// Token: 0x06006D31 RID: 27953 RVA: 0x00299DB8 File Offset: 0x00297FB8
		private void Zero(int index)
		{
			this.m_Storage[index] = default(T);
		}

		// Token: 0x06006D32 RID: 27954 RVA: 0x0004A6C4 File Offset: 0x000488C4
		public T Peek(int idxofs = 0)
		{
			return this.m_Storage[this.m_HeadIdx - 1 - idxofs];
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x0004A6DB File Offset: 0x000488DB
		public void Set(int idxofs, T item)
		{
			this.m_Storage[this.m_HeadIdx - 1 - idxofs] = item;
		}

		// Token: 0x06006D34 RID: 27956 RVA: 0x0004A6F3 File Offset: 0x000488F3
		public void CropAtCount(int p)
		{
			this.RemoveLast(this.Count - p);
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x00299DDC File Offset: 0x00297FDC
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

		// Token: 0x06006D36 RID: 27958 RVA: 0x00299E38 File Offset: 0x00298038
		public T Pop()
		{
			this.m_HeadIdx--;
			T result = this.m_Storage[this.m_HeadIdx];
			this.m_Storage[this.m_HeadIdx] = default(T);
			return result;
		}

		// Token: 0x06006D37 RID: 27959 RVA: 0x0004A703 File Offset: 0x00048903
		public void Clear()
		{
			Array.Clear(this.m_Storage, 0, this.m_Storage.Length);
			this.m_HeadIdx = 0;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06006D38 RID: 27960 RVA: 0x0004A720 File Offset: 0x00048920
		public int Count
		{
			get
			{
				return this.m_HeadIdx;
			}
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x0001C722 File Offset: 0x0001A922
		int IList<!0>.IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x0001C722 File Offset: 0x0001A922
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x0001C722 File Offset: 0x0001A922
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000A14 RID: 2580
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

		// Token: 0x06006D3E RID: 27966 RVA: 0x0004A73B File Offset: 0x0004893B
		void ICollection<!0>.Add(T item)
		{
			this.Push(item);
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x0004A745 File Offset: 0x00048945
		void ICollection<!0>.Clear()
		{
			this.Clear();
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x0001C722 File Offset: 0x0001A922
		bool ICollection<!0>.Contains(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x0001C722 File Offset: 0x0001A922
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06006D42 RID: 27970 RVA: 0x0004A74D File Offset: 0x0004894D
		int ICollection<!0>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06006D43 RID: 27971 RVA: 0x00004050 File Offset: 0x00002250
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x0001C722 File Offset: 0x0001A922
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x0001C722 File Offset: 0x0001A922
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x0001C722 File Offset: 0x0001A922
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400621E RID: 25118
		private T[] m_Storage;

		// Token: 0x0400621F RID: 25119
		private int m_HeadIdx;
	}
}
