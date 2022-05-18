using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02001184 RID: 4484
	internal class FastStackDynamic<T> : List<T>
	{
		// Token: 0x06006D47 RID: 27975 RVA: 0x0004A755 File Offset: 0x00048955
		public FastStackDynamic(int startingCapacity) : base(startingCapacity)
		{
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x0004A75E File Offset: 0x0004895E
		public void Set(int idxofs, T item)
		{
			base[base.Count - 1 - idxofs] = item;
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x0004A771 File Offset: 0x00048971
		public T Push(T item)
		{
			base.Add(item);
			return item;
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x00299E80 File Offset: 0x00298080
		public void Expand(int size)
		{
			for (int i = 0; i < size; i++)
			{
				base.Add(default(T));
			}
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x00299EA8 File Offset: 0x002980A8
		public void Zero(int index)
		{
			base[index] = default(T);
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x0004A77B File Offset: 0x0004897B
		public T Peek(int idxofs = 0)
		{
			return base[base.Count - 1 - idxofs];
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x0004A78D File Offset: 0x0004898D
		public void CropAtCount(int p)
		{
			this.RemoveLast(base.Count - p);
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x0004A79D File Offset: 0x0004899D
		public void RemoveLast(int cnt = 1)
		{
			if (cnt == 1)
			{
				base.RemoveAt(base.Count - 1);
				return;
			}
			base.RemoveRange(base.Count - cnt, cnt);
		}

		// Token: 0x06006D4F RID: 27983 RVA: 0x0004A7C1 File Offset: 0x000489C1
		public T Pop()
		{
			T result = base[base.Count - 1];
			base.RemoveAt(base.Count - 1);
			return result;
		}
	}
}
