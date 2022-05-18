using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200147C RID: 5244
	[Serializable]
	public class SharedRect : SharedVariable<Rect>
	{
		// Token: 0x06007E15 RID: 32277 RVA: 0x000553D6 File Offset: 0x000535D6
		public static implicit operator SharedRect(Rect value)
		{
			return new SharedRect
			{
				mValue = value
			};
		}
	}
}
