using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012E1 RID: 4833
	[Serializable]
	public class NarrativeData
	{
		// Token: 0x060075AB RID: 30123 RVA: 0x000502D8 File Offset: 0x0004E4D8
		public NarrativeData()
		{
			this.lines = new List<Line>();
		}

		// Token: 0x040066C1 RID: 26305
		[SerializeField]
		public List<Line> lines;
	}
}
