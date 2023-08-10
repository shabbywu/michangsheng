using UnityEngine;

public class TDGAAccount
{
	private static TDGAAccount account;

	public static TDGAAccount SetAccount(string accountId)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (account == null)
		{
			account = new TDGAAccount();
		}
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
		return account;
	}

	public void SetAccountName(string accountName)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public void SetAccountType(AccountType type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public void SetLevel(int level)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public void SetAge(int age)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public void SetGender(Gender type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if ((int)Application.platform != 0)
		{
			_ = Application.platform;
			_ = 7;
		}
	}

	public void SetGameServer(string gameServer)
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
