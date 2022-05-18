using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001477 RID: 5239
	[Serializable]
	public class SharedInt : SharedVariable<int>
	{
		// Token: 0x06007E0B RID: 32267 RVA: 0x00055368 File Offset: 0x00053568
		public static implicit operator SharedInt(int value)
		{
			return new SharedInt
			{
				mValue = value
			};
		}
	}
}
