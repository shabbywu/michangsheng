using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001476 RID: 5238
	[Serializable]
	public class SharedGameObjectList : SharedVariable<List<GameObject>>
	{
		// Token: 0x06007E09 RID: 32265 RVA: 0x00055352 File Offset: 0x00053552
		public static implicit operator SharedGameObjectList(List<GameObject> value)
		{
			return new SharedGameObjectList
			{
				mValue = value
			};
		}
	}
}
