using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001481 RID: 5249
	[Serializable]
	public class SharedVector3 : SharedVariable<Vector3>
	{
		// Token: 0x06007E1F RID: 32287 RVA: 0x00055444 File Offset: 0x00053644
		public static implicit operator SharedVector3(Vector3 value)
		{
			return new SharedVector3
			{
				mValue = value
			};
		}
	}
}
