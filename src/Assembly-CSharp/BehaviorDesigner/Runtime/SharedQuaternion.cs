using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200147B RID: 5243
	[Serializable]
	public class SharedQuaternion : SharedVariable<Quaternion>
	{
		// Token: 0x06007E13 RID: 32275 RVA: 0x000553C0 File Offset: 0x000535C0
		public static implicit operator SharedQuaternion(Quaternion value)
		{
			return new SharedQuaternion
			{
				mValue = value
			};
		}
	}
}
