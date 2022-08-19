using System;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class TDGAMission
{
	// Token: 0x06000F18 RID: 3864 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnBegin(string missionId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnCompleted(string missionId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnFailed(string missionId, string failedCause)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}
}
