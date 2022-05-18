using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class RealTime : MonoBehaviour
{
	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x060007DA RID: 2010 RVA: 0x0000A8D9 File Offset: 0x00008AD9
	public static float time
	{
		get
		{
			return Time.unscaledTime;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060007DB RID: 2011 RVA: 0x0000A8E0 File Offset: 0x00008AE0
	public static float deltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}
