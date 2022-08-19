using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC9 RID: 4041
	[Serializable]
	public class SharedVector3 : SharedVariable<Vector3>
	{
		// Token: 0x06007025 RID: 28709 RVA: 0x002A8DC5 File Offset: 0x002A6FC5
		public static implicit operator SharedVector3(Vector3 value)
		{
			return new SharedVector3
			{
				mValue = value
			};
		}
	}
}
