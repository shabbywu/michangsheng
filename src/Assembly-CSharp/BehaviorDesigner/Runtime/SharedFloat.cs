using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FBC RID: 4028
	[Serializable]
	public class SharedFloat : SharedVariable<float>
	{
		// Token: 0x0600700B RID: 28683 RVA: 0x002A8CA7 File Offset: 0x002A6EA7
		public static implicit operator SharedFloat(float value)
		{
			return new SharedFloat
			{
				Value = value
			};
		}
	}
}
