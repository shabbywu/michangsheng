using System;

namespace MarkerMetro.Unity.WinLegacy.Reflection
{
	// Token: 0x02000C91 RID: 3217
	[Flags]
	public enum BindingFlags
	{
		// Token: 0x0400522F RID: 21039
		Default = 0,
		// Token: 0x04005230 RID: 21040
		Public = 1,
		// Token: 0x04005231 RID: 21041
		Instance = 2,
		// Token: 0x04005232 RID: 21042
		InvokeMethod = 3,
		// Token: 0x04005233 RID: 21043
		NonPublic = 4,
		// Token: 0x04005234 RID: 21044
		Static = 5,
		// Token: 0x04005235 RID: 21045
		FlattenHierarchy = 6,
		// Token: 0x04005236 RID: 21046
		DeclaredOnly = 7,
		// Token: 0x04005237 RID: 21047
		IgnoreCase = 8
	}
}
