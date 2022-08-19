using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC2 RID: 4034
	[Serializable]
	public class SharedObjectList : SharedVariable<List<Object>>
	{
		// Token: 0x06007017 RID: 28695 RVA: 0x002A8D2B File Offset: 0x002A6F2B
		public static implicit operator SharedObjectList(List<Object> value)
		{
			return new SharedObjectList
			{
				mValue = value
			};
		}
	}
}
