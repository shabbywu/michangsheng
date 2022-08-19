using System;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FBF RID: 4031
	[Serializable]
	public class SharedInt : SharedVariable<int>
	{
		// Token: 0x06007011 RID: 28689 RVA: 0x002A8CE9 File Offset: 0x002A6EE9
		public static implicit operator SharedInt(int value)
		{
			return new SharedInt
			{
				mValue = value
			};
		}
	}
}
