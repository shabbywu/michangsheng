using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001482 RID: 5250
	[Serializable]
	public class SharedVector4 : SharedVariable<Vector4>
	{
		// Token: 0x06007E21 RID: 32289 RVA: 0x0005545A File Offset: 0x0005365A
		public static implicit operator SharedVector4(Vector4 value)
		{
			return new SharedVector4
			{
				mValue = value
			};
		}
	}
}
