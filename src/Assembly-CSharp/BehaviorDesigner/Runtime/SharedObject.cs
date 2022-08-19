using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC1 RID: 4033
	[Serializable]
	public class SharedObject : SharedVariable<Object>
	{
		// Token: 0x06007015 RID: 28693 RVA: 0x002A8D15 File Offset: 0x002A6F15
		public static explicit operator SharedObject(Object value)
		{
			return new SharedObject
			{
				mValue = value
			};
		}
	}
}
