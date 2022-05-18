using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x0200147A RID: 5242
	[Serializable]
	public class SharedObjectList : SharedVariable<List<Object>>
	{
		// Token: 0x06007E11 RID: 32273 RVA: 0x000553AA File Offset: 0x000535AA
		public static implicit operator SharedObjectList(List<Object> value)
		{
			return new SharedObjectList
			{
				mValue = value
			};
		}
	}
}
