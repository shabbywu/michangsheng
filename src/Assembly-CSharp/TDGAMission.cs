using System;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class TDGAMission
{
	// Token: 0x06001144 RID: 4420 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnBegin(string missionId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnCompleted(string missionId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnFailed(string missionId, string failedCause)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}
}
