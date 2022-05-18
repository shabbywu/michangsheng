using System;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class TDGAItem
{
	// Token: 0x06001141 RID: 4417 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnPurchase(string item, int itemNumber, double priceInVirtualCurrency)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x00010C59 File Offset: 0x0000EE59
	public static void OnUse(string item, int itemNumber)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}
}
