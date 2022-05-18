using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001474 RID: 5236
	[Serializable]
	public class SharedFloat : SharedVariable<float>
	{
		// Token: 0x06007E05 RID: 32261 RVA: 0x00055326 File Offset: 0x00053526
		public static implicit operator SharedFloat(float value)
		{
			return new SharedFloat
			{
				Value = value
			};
		}
	}
}
