using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class TDGAVirtualCurrency
{
	// Token: 0x06000F1C RID: 3868 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnChargeRequest(string orderId, string iapId, double currencyAmount, string currencyType, double virtualCurrencyAmount, string paymentType)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnChargeSuccess(string orderId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnReward(double virtualCurrencyAmount, string reason)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}
}
