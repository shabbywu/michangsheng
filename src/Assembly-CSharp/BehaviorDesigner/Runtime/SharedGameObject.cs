using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001475 RID: 5237
	[Serializable]
	public class SharedGameObject : SharedVariable<GameObject>
	{
		// Token: 0x06007E07 RID: 32263 RVA: 0x0005533C File Offset: 0x0005353C
		public static implicit operator SharedGameObject(GameObject value)
		{
			return new SharedGameObject
			{
				mValue = value
			};
		}
	}
}
