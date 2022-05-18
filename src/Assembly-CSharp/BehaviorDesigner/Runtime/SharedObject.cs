using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02001479 RID: 5241
	[Serializable]
	public class SharedObject : SharedVariable<Object>
	{
		// Token: 0x06007E0F RID: 32271 RVA: 0x00055394 File Offset: 0x00053594
		public static explicit operator SharedObject(Object value)
		{
			return new SharedObject
			{
				mValue = value
			};
		}
	}
}
