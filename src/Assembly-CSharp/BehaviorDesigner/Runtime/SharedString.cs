using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200147D RID: 5245
	[Serializable]
	public class SharedString : SharedVariable<string>
	{
		// Token: 0x06007E17 RID: 32279 RVA: 0x000553EC File Offset: 0x000535EC
		public static implicit operator SharedString(string value)
		{
			return new SharedString
			{
				mValue = value
			};
		}
	}
}
