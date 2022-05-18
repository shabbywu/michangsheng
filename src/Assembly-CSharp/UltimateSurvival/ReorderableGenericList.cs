using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000858 RID: 2136
	[Serializable]
	public class ReorderableGenericList<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000599 RID: 1433
		public T this[int key]
		{
			get
			{
				return this.m_List[key];
			}
			set
			{
				this.m_List[key] = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000286AB File Offset: 0x000268AB
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000286B8 File Offset: 0x000268B8
		public List<T> List
		{
			get
			{
				return this.m_List;
			}
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000286C0 File Offset: 0x000268C0
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_List.GetEnumerator();
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000286D2 File Offset: 0x000268D2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040031F3 RID: 12787
		[SerializeField]
		private List<T> m_List;
	}
}
