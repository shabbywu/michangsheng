using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class TalkingDataGA
{
	// Token: 0x06001130 RID: 4400 RVA: 0x00010C3C File Offset: 0x0000EE3C
	public static string GetDeviceId()
	{
		if (TalkingDataGA.deviceId == null && Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
		return TalkingDataGA.deviceId;
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnStart(string appID, string channelId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnEnd()
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnKill()
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnEvent(string actionId, Dictionary<string, object> parameters)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void SetVerboseLogDisabled()
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x04000DE9 RID: 3561
	private static string deviceId;
}
