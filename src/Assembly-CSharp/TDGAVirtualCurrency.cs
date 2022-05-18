using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class TDGAVirtualCurrency
{
	// Token: 0x06001148 RID: 4424 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnChargeRequest(string orderId, string iapId, double currencyAmount, string currencyType, double virtualCurrencyAmount, string paymentType)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnChargeSuccess(string orderId)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnReward(double virtualCurrencyAmount, string reason)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}
}
