using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022A RID: 554
public class TalkingDataDemoScript : MonoBehaviour
{
	// Token: 0x06001128 RID: 4392 RVA: 0x000AB4B4 File Offset: 0x000A96B4
	private void OnGUI()
	{
		int num = 0;
		GUI.Box(new Rect(10f, 10f, (float)(Screen.width - 20), (float)(Screen.height - 20)), "Demo Menu");
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Create User"))
		{
			this.account = TDGAAccount.SetAccount("User" + this.index);
			this.index++;
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Set Account Type") && this.account != null)
		{
			this.account.SetAccountType(AccountType.WEIXIN);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Account Level +1") && this.account != null)
		{
			TDGAAccount tdgaaccount = this.account;
			int num2 = this.level;
			this.level = num2 + 1;
			tdgaaccount.SetLevel(num2);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Chagen Game Server + 'a'") && this.account != null)
		{
			this.gameserver += "a";
			this.account.SetGameServer(this.gameserver);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Charge Request 10"))
		{
			TDGAVirtualCurrency.OnChargeRequest("order01", "iap", 10.0, "CH", 10.0, "PT");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Charge Success 10"))
		{
			TDGAVirtualCurrency.OnChargeSuccess("order01");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Reward 100"))
		{
			TDGAVirtualCurrency.OnReward(100.0, "reason");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Mission Begin"))
		{
			TDGAMission.OnBegin("miss001");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Mission Completed"))
		{
			TDGAMission.OnCompleted("miss001");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Item Purchase 10"))
		{
			TDGAItem.OnPurchase("itemid001", 10, 10.0);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Item Use 1"))
		{
			TDGAItem.OnUse("itemid001", 1);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)this.width, 50f), "Custome Event"))
		{
			TalkingDataGA.OnEvent("action_id", new Dictionary<string, object>
			{
				{
					"StartAppStartAppTime",
					"startAppMac#02/01/2013 09:52:24"
				},
				{
					"IntValue",
					1
				}
			});
		}
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x00010B9A File Offset: 0x0000ED9A
	private void Start()
	{
		Debug.Log("start...!!!!!!!!!!");
		TalkingDataGA.OnStart("0A33A9FA393A4EC898A788FC293DDD94", "your_channel_id");
		this.account = TDGAAccount.SetAccount("User01");
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x00010BC5 File Offset: 0x0000EDC5
	private void Update()
	{
		if (Input.GetKey(27))
		{
			Application.Quit();
		}
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x00010BD5 File Offset: 0x0000EDD5
	private void OnDestroy()
	{
		TalkingDataGA.OnEnd();
		Debug.Log("onDestroy");
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x00010BE6 File Offset: 0x0000EDE6
	private void Awake()
	{
		Debug.Log("Awake");
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x00010BF2 File Offset: 0x0000EDF2
	private void OnEnable()
	{
		Debug.Log("OnEnable");
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x00010BFE File Offset: 0x0000EDFE
	private void OnDisable()
	{
		Debug.Log("OnDisable");
	}

	// Token: 0x04000DE0 RID: 3552
	private int index = 1;

	// Token: 0x04000DE1 RID: 3553
	private int level = 1;

	// Token: 0x04000DE2 RID: 3554
	private string gameserver = "";

	// Token: 0x04000DE3 RID: 3555
	private TDGAAccount account;

	// Token: 0x04000DE4 RID: 3556
	private const int left = 90;

	// Token: 0x04000DE5 RID: 3557
	private const int height = 50;

	// Token: 0x04000DE6 RID: 3558
	private const int top = 120;

	// Token: 0x04000DE7 RID: 3559
	private int width = Screen.width - 180;

	// Token: 0x04000DE8 RID: 3560
	private const int step = 60;
}
