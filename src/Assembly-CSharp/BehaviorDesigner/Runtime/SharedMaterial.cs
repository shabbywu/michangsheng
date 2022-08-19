using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC0 RID: 4032
	[Serializable]
	public class SharedMaterial : SharedVariable<Material>
	{
		// Token: 0x06007013 RID: 28691 RVA: 0x002A8CFF File Offset: 0x002A6EFF
		public static implicit operator SharedMaterial(Material value)
		{
			return new SharedMaterial
			{
				mValue = value
			};
		}
	}
}
