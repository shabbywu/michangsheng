using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FB9 RID: 4025
	[Serializable]
	public class SharedAnimationCurve : SharedVariable<AnimationCurve>
	{
		// Token: 0x06007005 RID: 28677 RVA: 0x002A8C65 File Offset: 0x002A6E65
		public static implicit operator SharedAnimationCurve(AnimationCurve value)
		{
			return new SharedAnimationCurve
			{
				mValue = value
			};
		}
	}
}
