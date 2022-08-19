using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC3 RID: 4035
	[Serializable]
	public class SharedQuaternion : SharedVariable<Quaternion>
	{
		// Token: 0x06007019 RID: 28697 RVA: 0x002A8D41 File Offset: 0x002A6F41
		public static implicit operator SharedQuaternion(Quaternion value)
		{
			return new SharedQuaternion
			{
				mValue = value
			};
		}
	}
}
