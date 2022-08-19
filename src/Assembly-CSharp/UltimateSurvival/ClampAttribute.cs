using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005A8 RID: 1448
	public class ClampAttribute : PropertyAttribute
	{
		// Token: 0x06002F3E RID: 12094 RVA: 0x001566B6 File Offset: 0x001548B6
		public ClampAttribute(float min, float max)
		{
			this.ClampLimits = new Vector2(min, max);
		}

		// Token: 0x04002976 RID: 10614
		public readonly Vector2 ClampLimits;
	}
}
