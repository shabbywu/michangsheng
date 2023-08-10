using UnityEngine;

public class TDGAVirtualCurrency
{
	public static void OnChargeRequest(string orderId, string iapId, double currencyAmount, string currencyType, double virtualCurrencyAmount, string paymentType)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public static void OnChargeSuccess(string orderId)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public static void OnReward(double virtualCurrencyAmount, string reason)
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
