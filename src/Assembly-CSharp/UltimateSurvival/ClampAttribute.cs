using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000864 RID: 2148
	public class ClampAttribute : PropertyAttribute
	{
		// Token: 0x060037BC RID: 14268 RVA: 0x0002872A File Offset: 0x0002692A
		public ClampAttribute(float min, float max)
		{
			this.ClampLimits = new Vector2(min, max);
		}

		// Token: 0x040031F7 RID: 12791
		public readonly Vector2 ClampLimits;
	}
}
