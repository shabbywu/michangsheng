using System;
using UnityEngine;

// Token: 0x02000157 RID: 343
public class TDGAAccount
{
	// Token: 0x06000F0C RID: 3852 RVA: 0x0005B7B9 File Offset: 0x000599B9
	public static TDGAAccount SetAccount(string accountId)
	{
		if (TDGAAccount.account == null)
		{
			TDGAAccount.account = new TDGAAccount();
		}
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
		return TDGAAccount.account;
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public void SetAccountName(string accountName)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public void SetAccountType(AccountType type)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public void SetLevel(int level)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public void SetAge(int age)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public void SetGender(Gender type)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0005B7A8 File Offset: 0x000599A8
	public void SetGameServer(string gameServer)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x04000B5B RID: 2907
	private static TDGAAccount account;
}
