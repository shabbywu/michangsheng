using System;

namespace MarkerMetro.Unity.WinLegacy.Reflection
{
	// Token: 0x02001056 RID: 4182
	[Flags]
	public enum BindingFlags
	{
		// Token: 0x04005DDD RID: 24029
		Default = 0,
		// Token: 0x04005DDE RID: 24030
		Public = 1,
		// Token: 0x04005DDF RID: 24031
		Instance = 2,
		// Token: 0x04005DE0 RID: 24032
		InvokeMethod = 3,
		// Token: 0x04005DE1 RID: 24033
		NonPublic = 4,
		// Token: 0x04005DE2 RID: 24034
		Static = 5,
		// Token: 0x04005DE3 RID: 24035
		FlattenHierarchy = 6,
		// Token: 0x04005DE4 RID: 24036
		DeclaredOnly = 7,
		// Token: 0x04005DE5 RID: 24037
		IgnoreCase = 8
	}
}
