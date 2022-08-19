using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.DataStructs
{
	// Token: 0x02000D6E RID: 3438
	internal class FastStackDynamic<T> : List<T>
	{
		// Token: 0x06006153 RID: 24915 RVA: 0x002733B0 File Offset: 0x002715B0
		public FastStackDynamic(int startingCapacity) : base(startingCapacity)
		{
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x002733B9 File Offset: 0x002715B9
		public void Set(int idxofs, T item)
		{
			base[base.Count - 1 - idxofs] = item;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x002733CC File Offset: 0x002715CC
		public T Push(T item)
		{
			base.Add(item);
			return item;
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x002733D8 File Offset: 0x002715D8
		public void Expand(int size)
		{
			for (int i = 0; i < size; i++)
			{
				base.Add(default(T));
			}
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x00273400 File Offset: 0x00271600
		public void Zero(int index)
		{
			base[index] = default(T);
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x0027341D File Offset: 0x0027161D
		public T Peek(int idxofs = 0)
		{
			return base[base.Count - 1 - idxofs];
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0027342F File Offset: 0x0027162F
		public void CropAtCount(int p)
		{
			this.RemoveLast(base.Count - p);
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x0027343F File Offset: 0x0027163F
		public void RemoveLast(int cnt = 1)
		{
			if (cnt == 1)
			{
				base.RemoveAt(base.Count - 1);
				return;
			}
			base.RemoveRange(base.Count - cnt, cnt);
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x00273463 File Offset: 0x00271663
		public T Pop()
		{
			T result = base[base.Count - 1];
			base.RemoveAt(base.Count - 1);
			return result;
		}
	}
}
