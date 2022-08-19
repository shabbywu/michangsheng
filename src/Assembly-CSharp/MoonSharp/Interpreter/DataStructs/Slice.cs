using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02000D72 RID: 3442
	internal class Slice<T> : IEnumerable<!0>, IEnumerable, IList<!0>, ICollection<!0>
	{
		// Token: 0x0600616F RID: 24943 RVA: 0x002736B9 File Offset: 0x002718B9
		public Slice(IList<T> list, int from, int length, bool reversed)
		{
			this.m_SourceList = list;
			this.m_From = from;
			this.m_Length = length;
			this.m_Reversed = reversed;
		}

		// Token: 0x170007BB RID: 1979
		public T this[int index]
		{
			get
			{
				return this.m_SourceList[this.CalcRealIndex(index)];
			}
			set
			{
				this.m_SourceList[this.CalcRealIndex(index)] = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06006172 RID: 24946 RVA: 0x00273707 File Offset: 0x00271907
		public int From
		{
			get
			{
				return this.m_From;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06006173 RID: 24947 RVA: 0x0027370F File Offset: 0x0027190F
		public int Count
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06006174 RID: 24948 RVA: 0x00273717 File Offset: 0x00271917
		public bool Reversed
		{
			get
			{
				return this.m_Reversed;
			}
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0027371F File Offset: 0x0027191F
		private int CalcRealIndex(int index)
		{
			if (index < 0 || index >= this.m_Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.m_Reversed)
			{
				return this.m_From + this.m_Length - index - 1;
			}
			return this.m_From + index;
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x0027375B File Offset: 0x0027195B
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_Length; i = num + 1)
			{
				yield return this.m_SourceList[this.CalcRealIndex(i)];
				num = i;
			}
			yield break;
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x0027376A File Offset: 0x0027196A
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.m_Length; i = num + 1)
			{
				yield return this.m_SourceList[this.CalcRealIndex(i)];
				num = i;
			}
			yield break;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x0027377C File Offset: 0x0027197C
		public T[] ToArray()
		{
			T[] array = new T[this.m_Length];
			for (int i = 0; i < this.m_Length; i++)
			{
				array[i] = this.m_SourceList[this.CalcRealIndex(i)];
			}
			return array;
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x002737C0 File Offset: 0x002719C0
		public List<T> ToList()
		{
			List<T> list = new List<T>(this.m_Length);
			for (int i = 0; i < this.m_Length; i++)
			{
				list.Add(this.m_SourceList[this.CalcRealIndex(i)]);
			}
			return list;
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x00273804 File Offset: 0x00271A04
		public int IndexOf(T item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				T t = this[i];
				if (t.Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x00273842 File Offset: 0x00271A42
		public void Insert(int index, T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x00273842 File Offset: 0x00271A42
		public void RemoveAt(int index)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x00273842 File Offset: 0x00271A42
		public void Add(T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x0600617E RID: 24958 RVA: 0x00273842 File Offset: 0x00271A42
		public void Clear()
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x0027384E File Offset: 0x00271A4E
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x00273860 File Offset: 0x00271A60
		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[i + arrayIndex] = this[i];
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06006181 RID: 24961 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x00273842 File Offset: 0x00271A42
		public bool Remove(T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x04005589 RID: 21897
		private IList<T> m_SourceList;

		// Token: 0x0400558A RID: 21898
		private int m_From;

		// Token: 0x0400558B RID: 21899
		private int m_Length;

		// Token: 0x0400558C RID: 21900
		private bool m_Reversed;
	}
}
