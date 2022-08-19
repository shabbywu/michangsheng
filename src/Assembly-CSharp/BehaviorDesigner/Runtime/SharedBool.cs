using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FBA RID: 4026
	[Serializable]
	public class SharedBool : SharedVariable<bool>
	{
		// Token: 0x06007007 RID: 28679 RVA: 0x002A8C7B File Offset: 0x002A6E7B
		public static implicit operator SharedBool(bool value)
		{
			return new SharedBool
			{
				mValue = value
			};
		}
	}
}
