using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class TDGAItem
{
	// Token: 0x06000F15 RID: 3861 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnPurchase(string item, int itemNumber, double priceInVirtualCurrency)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public static void OnUse(string item, int itemNumber)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}
}
