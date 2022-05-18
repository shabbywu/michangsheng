using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001473 RID: 5235
	[Serializable]
	public class SharedColor : SharedVariable<Color>
	{
		// Token: 0x06007E03 RID: 32259 RVA: 0x00055310 File Offset: 0x00053510
		public static implicit operator SharedColor(Color value)
		{
			return new SharedColor
			{
				mValue = value
			};
		}
	}
}
