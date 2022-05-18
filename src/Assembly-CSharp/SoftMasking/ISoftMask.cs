using System;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x02000A07 RID: 2567
	public interface ISoftMask
	{
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06004282 RID: 17026
		bool isAlive { get; }

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06004283 RID: 17027
		bool isMaskingEnabled { get; }

		// Token: 0x06004284 RID: 17028
		Material GetReplacement(Material original);

		// Token: 0x06004285 RID: 17029
		void ReleaseReplacement(Material replacement);

		// Token: 0x06004286 RID: 17030
		void UpdateTransformChildren(Transform transform);
	}
}
