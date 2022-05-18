using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x02001483 RID: 5251
	public class FloatSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x06007E23 RID: 32291 RVA: 0x00055470 File Offset: 0x00053670
		public FloatSliderAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04006B68 RID: 27496
		public float min;

		// Token: 0x04006B69 RID: 27497
		public float max;
	}
}
