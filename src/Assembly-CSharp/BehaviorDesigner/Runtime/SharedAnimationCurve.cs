using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001471 RID: 5233
	[Serializable]
	public class SharedAnimationCurve : SharedVariable<AnimationCurve>
	{
		// Token: 0x06007DFF RID: 32255 RVA: 0x000552E4 File Offset: 0x000534E4
		public static implicit operator SharedAnimationCurve(AnimationCurve value)
		{
			return new SharedAnimationCurve
			{
				mValue = value
			};
		}
	}
}
