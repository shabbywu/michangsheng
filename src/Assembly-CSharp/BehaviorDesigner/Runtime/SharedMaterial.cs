using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001478 RID: 5240
	[Serializable]
	public class SharedMaterial : SharedVariable<Material>
	{
		// Token: 0x06007E0D RID: 32269 RVA: 0x0005537E File Offset: 0x0005357E
		public static implicit operator SharedMaterial(Material value)
		{
			return new SharedMaterial
			{
				mValue = value
			};
		}
	}
}
