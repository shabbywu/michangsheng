using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class RealTime : MonoBehaviour
{
	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06000757 RID: 1879 RVA: 0x0002B6EF File Offset: 0x000298EF
	public static float time
	{
		get
		{
			return Time.unscaledTime;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06000758 RID: 1880 RVA: 0x0002B6F6 File Offset: 0x000298F6
	public static float deltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}
