using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FBD RID: 4029
	[Serializable]
	public class SharedGameObject : SharedVariable<GameObject>
	{
		// Token: 0x0600700D RID: 28685 RVA: 0x002A8CBD File Offset: 0x002A6EBD
		public static implicit operator SharedGameObject(GameObject value)
		{
			return new SharedGameObject
			{
				mValue = value
			};
		}
	}
}
