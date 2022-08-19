using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x02000FCC RID: 4044
	public class IntSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x0600702A RID: 28714 RVA: 0x002A8E07 File Offset: 0x002A7007
		public IntSliderAttribute(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04005C72 RID: 23666
		public int min;

		// Token: 0x04005C73 RID: 23667
		public int max;
	}
}
