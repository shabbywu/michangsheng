using System;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class TDGAAccount
{
	// Token: 0x06001138 RID: 4408 RVA: 0x00010C6A File Offset: 0x0000EE6A
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

	// Token: 0x0600113A RID: 4410 RVA: 0x00010C59 File Offset: 0x0000EE59
	public void SetAccountName(string accountName)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x00010C59 File Offset: 0x0000EE59
	public void SetAccountType(AccountType type)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x00010C59 File Offset: 0x0000EE59
	public void SetLevel(int level)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x00010C59 File Offset: 0x0000EE59
	public void SetAge(int age)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x00010C59 File Offset: 0x0000EE59
	public void SetGender(Gender type)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x00010C59 File Offset: 0x0000EE59
	public void SetGameServer(string gameServer)
	{
		if (Application.platform != null)
		{
			RuntimePlatform platform = Application.platform;
		}
	}

	// Token: 0x04000E00 RID: 3584
	private static TDGAAccount account;
}
