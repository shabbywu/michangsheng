using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200059C RID: 1436
	[Serializable]
	public class ReorderableGenericList<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x170003E0 RID: 992
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

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x00156637 File Offset: 0x00154837
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x00156644 File Offset: 0x00154844
		public List<T> List
		{
			get
			{
				return this.m_List;
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x0015664C File Offset: 0x0015484C
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_List.GetEnumerator();
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x0015665E File Offset: 0x0015485E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002972 RID: 10610
		[SerializeField]
		private List<T> m_List;
	}
}
