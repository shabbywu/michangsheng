using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime
{
	// Token: 0x02000FC4 RID: 4036
	[Serializable]
	public class SharedRect : SharedVariable<Rect>
	{
		// Token: 0x0600701B RID: 28699 RVA: 0x002A8D57 File Offset: 0x002A6F57
		public static implicit operator SharedRect(Rect value)
		{
			return new SharedRect
			{
				mValue = value
			};
		}
	}
}
