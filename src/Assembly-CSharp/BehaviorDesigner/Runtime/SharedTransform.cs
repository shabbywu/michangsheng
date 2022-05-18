using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200147E RID: 5246
	[Serializable]
	public class SharedTransform : SharedVariable<Transform>
	{
		// Token: 0x06007E19 RID: 32281 RVA: 0x00055402 File Offset: 0x00053602
		public static implicit operator SharedTransform(Transform value)
		{
			return new SharedTransform
			{
				mValue = value
			};
		}
	}
}
