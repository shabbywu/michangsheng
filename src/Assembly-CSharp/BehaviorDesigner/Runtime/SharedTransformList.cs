using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200147F RID: 5247
	[Serializable]
	public class SharedTransformList : SharedVariable<List<Transform>>
	{
		// Token: 0x06007E1B RID: 32283 RVA: 0x00055418 File Offset: 0x00053618
		public static implicit operator SharedTransformList(List<Transform> value)
		{
			return new SharedTransformList
			{
				mValue = value
			};
		}
	}
}
