using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FBB RID: 4027
	[Serializable]
	public class SharedColor : SharedVariable<Color>
	{
		// Token: 0x06007009 RID: 28681 RVA: 0x002A8C91 File Offset: 0x002A6E91
		public static implicit operator SharedColor(Color value)
		{
			return new SharedColor
			{
				mValue = value
			};
		}
	}
}
