using System;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x020006DC RID: 1756
	public interface IMaterialReplacer
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600386B RID: 14443
		int order { get; }

		// Token: 0x0600386C RID: 14444
		Material Replace(Material material);
	}
}
