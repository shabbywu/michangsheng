using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x02001484 RID: 5252
	public class IntSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x06007E24 RID: 32292 RVA: 0x00055486 File Offset: 0x00053686
		public IntSliderAttribute(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04006B6A RID: 27498
		public int min;

		// Token: 0x04006B6B RID: 27499
		public int max;
	}
}
