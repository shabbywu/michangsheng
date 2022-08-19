using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x02000FCB RID: 4043
	public class FloatSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x06007029 RID: 28713 RVA: 0x002A8DF1 File Offset: 0x002A6FF1
		public FloatSliderAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04005C70 RID: 23664
		public float min;

		// Token: 0x04005C71 RID: 23665
		public float max;
	}
}
