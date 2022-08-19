using System;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x020006D9 RID: 1753
	public interface ISoftMask
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06003860 RID: 14432
		bool isAlive { get; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06003861 RID: 14433
		bool isMaskingEnabled { get; }

		// Token: 0x06003862 RID: 14434
		Material GetReplacement(Material original);

		// Token: 0x06003863 RID: 14435
		void ReleaseReplacement(Material replacement);

		// Token: 0x06003864 RID: 14436
		void UpdateTransformChildren(Transform transform);
	}
}
