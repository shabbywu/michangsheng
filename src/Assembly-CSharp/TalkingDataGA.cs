using System.Collections.Generic;
using UnityEngine;

public class TalkingDataGA
{
	private static string deviceId;

	public static string GetDeviceId()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (deviceId == null && (int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
		return deviceId;
	}

	public static void OnStart(string appID, string channelId)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public static void OnEnd()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public static void OnKill()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public static void OnEvent(string actionId, Dictionary<string, object> parameters)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public static void SetVerboseLogDisabled()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}
}
