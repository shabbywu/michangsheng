using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class TalkingDataGA
{
	// Token: 0x06000F04 RID: 3844 RVA: 0x0005B78B File Offset: 0x0005998B
	public static string GetDeviceId()
	{
		if (TalkingDataGA.deviceId == null && Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
		return TalkingDataGA.deviceId;
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnStart(string appID, string channelId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnEnd()
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnKill()
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnEvent(string actionId, Dictionary<string, object> parameters)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void SetVerboseLogDisabled()
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x04000B44 RID: 2884
	private static string deviceId;
}
