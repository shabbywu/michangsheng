using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class TalkingDataDemoScript : MonoBehaviour
{
	// Token: 0x06000EFC RID: 3836 RVA: 0x0005B344 File Offset: 0x00059544
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

	// Token: 0x06000EFD RID: 3837 RVA: 0x0005B6E9 File Offset: 0x000598E9
	private void Start()
	{
		Debug.Log("start...!!!!!!!!!!");
		TalkingDataGA.OnStart("0A33A9FA393A4EC898A788FC293DDD94", "your_channel_id");
		this.account = TDGAAccount.SetAccount("User01");
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0005B714 File Offset: 0x00059914
	private void Update()
	{
		if (Input.GetKey(27))
		{
			Application.Quit();
		}
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0005B724 File Offset: 0x00059924
	private void OnDestroy()
	{
		TalkingDataGA.OnEnd();
		Debug.Log("onDestroy");
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x0005B735 File Offset: 0x00059935
	private void Awake()
	{
		Debug.Log("Awake");
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x0005B741 File Offset: 0x00059941
	private void OnEnable()
	{
		Debug.Log("OnEnable");
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x0005B74D File Offset: 0x0005994D
	private void OnDisable()
	{
		Debug.Log("OnDisable");
	}

	// Token: 0x04000B3B RID: 2875
	private int index = 1;

	// Token: 0x04000B3C RID: 2876
	private int level = 1;

	// Token: 0x04000B3D RID: 2877
	private string gameserver = "";

	// Token: 0x04000B3E RID: 2878
	private TDGAAccount account;

	// Token: 0x04000B3F RID: 2879
	private const int left = 90;

	// Token: 0x04000B40 RID: 2880
	private const int height = 50;

	// Token: 0x04000B41 RID: 2881
	private const int top = 120;

	// Token: 0x04000B42 RID: 2882
	private int width = Screen.width - 180;

	// Token: 0x04000B43 RID: 2883
	private const int step = 60;
}
