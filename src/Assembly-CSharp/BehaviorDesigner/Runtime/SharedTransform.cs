using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC6 RID: 4038
	[Serializable]
	public class SharedTransform : SharedVariable<Transform>
	{
		// Token: 0x0600701F RID: 28703 RVA: 0x002A8D83 File Offset: 0x002A6F83
		public static implicit operator SharedTransform(Transform value)
		{
			return new SharedTransform
			{
				mValue = value
			};
		}
	}
}
