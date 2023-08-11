using System.Collections.Generic;
using UnityEngine;

public class TalkingDataDemoScript : MonoBehaviour
{
	private int index = 1;

	private int level = 1;

	private string gameserver = "";

	private TDGAAccount account;

	private const int left = 90;

	private const int height = 50;

	private const int top = 120;

	private int width = Screen.width - 180;

	private const int step = 60;

	private void OnGUI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		GUI.Box(new Rect(10f, 10f, (float)(Screen.width - 20), (float)(Screen.height - 20)), "Demo Menu");
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Create User"))
		{
			account = TDGAAccount.SetAccount("User" + index);
			index++;
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Set Account Type") && account != null)
		{
			account.SetAccountType(AccountType.WEIXIN);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Account Level +1") && account != null)
		{
			account.SetLevel(level++);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Chagen Game Server + 'a'") && account != null)
		{
			gameserver += "a";
			account.SetGameServer(gameserver);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Charge Request 10"))
		{
			TDGAVirtualCurrency.OnChargeRequest("order01", "iap", 10.0, "CH", 10.0, "PT");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Charge Success 10"))
		{
			TDGAVirtualCurrency.OnChargeSuccess("order01");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Reward 100"))
		{
			TDGAVirtualCurrency.OnReward(100.0, "reason");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Mission Begin"))
		{
			TDGAMission.OnBegin("miss001");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Mission Completed"))
		{
			TDGAMission.OnCompleted("miss001");
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Item Purchase 10"))
		{
			TDGAItem.OnPurchase("itemid001", 10, 10.0);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Item Use 1"))
		{
			TDGAItem.OnUse("itemid001", 1);
		}
		if (GUI.Button(new Rect(90f, (float)(120 + 60 * num++), (float)width, 50f), "Custome Event"))
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("StartAppStartAppTime", "startAppMac#02/01/2013 09:52:24");
			dictionary.Add("IntValue", 1);
			TalkingDataGA.OnEvent("action_id", dictionary);
		}
	}

	private void Start()
	{
		Debug.Log((object)"start...!!!!!!!!!!");
		TalkingDataGA.OnStart("0A33A9FA393A4EC898A788FC293DDD94", "your_channel_id");
		account = TDGAAccount.SetAccount("User01");
	}

	private void Update()
	{
		if (Input.GetKey((KeyCode)27))
		{
			Application.Quit();
		}
	}

	private void OnDestroy()
	{
		TalkingDataGA.OnEnd();
		Debug.Log((object)"onDestroy");
	}

	private void Awake()
	{
		Debug.Log((object)"Awake");
	}

	private void OnEnable()
	{
		Debug.Log((object)"OnEnable");
	}

	private void OnDisable()
	{
		Debug.Log((object)"OnDisable");
	}
}
