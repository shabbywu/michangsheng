using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001480 RID: 5248
	[Serializable]
	public class SharedVector2 : SharedVariable<Vector2>
	{
		// Token: 0x06007E1D RID: 32285 RVA: 0x0005542E File Offset: 0x0005362E
		public static implicit operator SharedVector2(Vector2 value)
		{
			return new SharedVector2
			{
				mValue = value
			};
		}
	}
}
