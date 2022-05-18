using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001472 RID: 5234
	[Serializable]
	public class SharedBool : SharedVariable<bool>
	{
		// Token: 0x06007E01 RID: 32257 RVA: 0x000552FA File Offset: 0x000534FA
		public static implicit operator SharedBool(bool value)
		{
			return new SharedBool
			{
				mValue = value
			};
		}
	}
}
