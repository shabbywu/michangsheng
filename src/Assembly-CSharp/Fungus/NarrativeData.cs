using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E77 RID: 3703
	[Serializable]
	public class NarrativeData
	{
		// Token: 0x060068C8 RID: 26824 RVA: 0x0028E483 File Offset: 0x0028C683
		public NarrativeData()
		{
			this.lines = new List<Line>();
		}

		// Token: 0x040058F9 RID: 22777
		[SerializeField]
		public List<Line> lines;
	}
}
