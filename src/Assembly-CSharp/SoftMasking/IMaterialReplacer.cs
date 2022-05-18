using System;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x02000A0B RID: 2571
	public interface IMaterialReplacer
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06004294 RID: 17044
		int order { get; }

		// Token: 0x06004295 RID: 17045
		Material Replace(Material material);
	}
}
