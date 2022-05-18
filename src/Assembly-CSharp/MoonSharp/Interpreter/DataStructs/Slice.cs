using System;
using System.Collections;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02001188 RID: 4488
	internal class Slice<T> : IEnumerable<!0>, IEnumerable, IList<!0>, ICollection<!0>
	{
		// Token: 0x06006D63 RID: 28003 RVA: 0x0004A89F File Offset: 0x00048A9F
		public Slice(IList<T> list, int from, int length, bool reversed)
		{
			this.m_SourceList = list;
			this.m_From = from;
			this.m_Length = length;
			this.m_Reversed = reversed;
		}

		// Token: 0x17000A18 RID: 2584
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06006D66 RID: 28006 RVA: 0x0004A8ED File Offset: 0x00048AED
		public int From
		{
			get
			{
				return this.m_From;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06006D67 RID: 28007 RVA: 0x0004A8F5 File Offset: 0x00048AF5
		public int Count
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06006D68 RID: 28008 RVA: 0x0004A8FD File Offset: 0x00048AFD
		public bool Reversed
		{
			get
			{
				return this.m_Reversed;
			}
		}

		// Token: 0x06006D69 RID: 28009 RVA: 0x0004A905 File Offset: 0x00048B05
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

		// Token: 0x06006D6A RID: 28010 RVA: 0x0004A941 File Offset: 0x00048B41
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

		// Token: 0x06006D6B RID: 28011 RVA: 0x0004A950 File Offset: 0x00048B50
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

		// Token: 0x06006D6C RID: 28012 RVA: 0x0029A044 File Offset: 0x00298244
		public T[] ToArray()
		{
			T[] array = new T[this.m_Length];
			for (int i = 0; i < this.m_Length; i++)
			{
				array[i] = this.m_SourceList[this.CalcRealIndex(i)];
			}
			return array;
		}

		// Token: 0x06006D6D RID: 28013 RVA: 0x0029A088 File Offset: 0x00298288
		public List<T> ToList()
		{
			List<T> list = new List<T>(this.m_Length);
			for (int i = 0; i < this.m_Length; i++)
			{
				list.Add(this.m_SourceList[this.CalcRealIndex(i)]);
			}
			return list;
		}

		// Token: 0x06006D6E RID: 28014 RVA: 0x0029A0CC File Offset: 0x002982CC
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

		// Token: 0x06006D6F RID: 28015 RVA: 0x0004A95F File Offset: 0x00048B5F
		public void Insert(int index, T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x0004A95F File Offset: 0x00048B5F
		public void RemoveAt(int index)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x06006D71 RID: 28017 RVA: 0x0004A95F File Offset: 0x00048B5F
		public void Add(T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x06006D72 RID: 28018 RVA: 0x0004A95F File Offset: 0x00048B5F
		public void Clear()
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x06006D73 RID: 28019 RVA: 0x0004A96B File Offset: 0x00048B6B
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06006D74 RID: 28020 RVA: 0x0029A10C File Offset: 0x0029830C
		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[i + arrayIndex] = this[i];
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06006D75 RID: 28021 RVA: 0x0000A093 File Offset: 0x00008293
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x0004A95F File Offset: 0x00048B5F
		public bool Remove(T item)
		{
			throw new InvalidOperationException("Slices are readonly");
		}

		// Token: 0x04006224 RID: 25124
		private IList<T> m_SourceList;

		// Token: 0x04006225 RID: 25125
		private int m_From;

		// Token: 0x04006226 RID: 25126
		private int m_Length;

		// Token: 0x04006227 RID: 25127
		private bool m_Reversed;
	}
}
