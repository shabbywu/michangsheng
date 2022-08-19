using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC8 RID: 4040
	[Serializable]
	public class SharedVector2 : SharedVariable<Vector2>
	{
		// Token: 0x06007023 RID: 28707 RVA: 0x002A8DAF File Offset: 0x002A6FAF
		public static implicit operator SharedVector2(Vector2 value)
		{
			return new SharedVector2
			{
				mValue = value
			};
		}
	}
}
