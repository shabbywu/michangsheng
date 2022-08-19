using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC5 RID: 4037
	[Serializable]
	public class SharedString : SharedVariable<string>
	{
		// Token: 0x0600701D RID: 28701 RVA: 0x002A8D6D File Offset: 0x002A6F6D
		public static implicit operator SharedString(string value)
		{
			return new SharedString
			{
				mValue = value
			};
		}
	}
}
