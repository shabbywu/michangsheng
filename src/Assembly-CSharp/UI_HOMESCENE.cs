using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_HOMESCENE : MonoBehaviour
{
	public Text UserName;

	public Text UserGold;

	public Text UserJade;

	private string TDpayOder;

	public ItemDatabase database;

	public GameObject ItemInspector;

	public GameObject ItemCollect;

	public GameObject ItemCheckIn;

	public GameObject camera;

	public GameObject buttonUI;

	public GameObject UICavase;

	public GameObject ShopUI;

	public GameObject CollectUI;

	public GameObject UIErrorMsg;

	public GameObject SetUI;

	public GameObject talkingUI;

	public GameObject FriendUI;

	public GameObject ChoiceUI;

	public GameObject TeamUI;

	private GameObject nowAvaterObj;

	public static UI_HOMESCENE instense;

	private FriendMgr friendmgr;

	private void Start()
	{
		instense = this;
		initUICavase();
		Event.registerOut("HomeErrorMessage", this, "HomeErrorMessage");
		Event.registerOut("createOder", this, "createOder");
		Event.registerOut("OpenFriendUI", this, "OpenFriendUI");
		Event.registerOut("showChiceUI", this, "showChiceUI");
		ShopUI.SetActive(false);
		CollectUI.SetActive(false);
		Random random = new Random();
		int num = 50 + random.Next() % 5;
		if (num == 52)
		{
			SetMainBackgroudAvater(53, 3);
		}
		else
		{
			SetMainBackgroudAvater(num, 3);
		}
		database = (ItemDatabase)(object)Resources.Load("Custom Data/Items/PlayerItem Database");
		((Account)KBEngineApp.app.player())?.onPayEnd();
	}

	public void initUICavase()
	{
		Account obj = (Account)KBEngineApp.app.player();
		Transform val = UICavase.transform.Find("ShopUI");
		Transform val2 = UICavase.transform.Find("CollectUI");
		Transform obj2 = UICavase.transform.Find("Text_error");
		Transform val3 = UICavase.transform.Find("setUI");
		Transform val4 = UICavase.transform.Find("talkingUI");
		Transform val5 = UICavase.transform.Find("friendUI");
		Transform val6 = UICavase.transform.Find("choiceUI");
		Transform val7 = UICavase.transform.Find("TeamUI");
		if ((Object)(object)val != (Object)null)
		{
			ShopUI = ((Component)val).gameObject;
		}
		if ((Object)(object)val2 != (Object)null)
		{
			CollectUI = ((Component)val2).gameObject;
		}
		if ((Object)(object)obj2 != (Object)null)
		{
			SetUI = ((Component)val3).gameObject;
		}
		if ((Object)(object)val4 != (Object)null)
		{
			talkingUI = ((Component)val4).gameObject;
		}
		if ((Object)(object)val5 != (Object)null)
		{
			FriendUI = ((Component)val5).gameObject;
		}
		if ((Object)(object)val6 != (Object)null)
		{
			ChoiceUI = ((Component)val6).gameObject;
		}
		if ((Object)(object)val6 != (Object)null)
		{
			TeamUI = ((Component)val7).gameObject;
		}
		PLAYER_INFO playerInfo = obj.playerInfo;
		Transform obj3 = SetUI.transform.Find("ItemSet");
		((Component)obj3.Find("name")).GetComponent<Text>().text = playerInfo.name;
		((Component)obj3.Find("Leave")).GetComponent<Text>().text = "等级:" + playerInfo.level;
		ushort exp = playerInfo.Exp;
		((Component)((Component)UserName).transform.Find("Button").GetChild(0)).GetComponent<Text>().text = playerInfo.name ?? "";
		float num = (int)exp;
		((Component)obj3.Find("Panel_target").Find("Slider_targetExp")).GetComponent<Slider>().value = num;
		((Component)obj3.Find("Panel_target").Find("Text_exp")).GetComponent<Text>().text = num + "/1000";
		CollectUI.SetActive(false);
		ShopUI.SetActive(false);
		SetUI.SetActive(false);
		talkingUI.SetActive(false);
		FriendUI.SetActive(false);
		ChoiceUI.SetActive(false);
		TeamUI.SetActive(true);
		friendmgr = new FriendMgr();
	}

	public void showChiceUI()
	{
	}

	public void addfriend()
	{
		Account account = (Account)KBEngineApp.app.player();
		string text = ((Component)FriendUI.transform.Find("InputField").Find("Text")).GetComponent<Text>().text;
		if (account != null && text != "")
		{
			account.addFriend(text);
		}
	}

	public void OpenPlayerInfoUI()
	{
		SetUI.SetActive(true);
	}

	public void OpenFriendUI()
	{
		friendmgr.OpenFriendUI();
		FriendUI.SetActive(true);
	}

	public void closeFrendUI()
	{
		FriendUI.SetActive(false);
	}

	public void closeTalkingUI()
	{
		talkingUI.SetActive(false);
	}

	public void closePlayerInfoUI()
	{
		SetUI.SetActive(false);
	}

	public void HomeErrorMessage(string msg)
	{
		UIPopTip.Inst.Pop(msg);
	}

	public void useCollect()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			Tooltip component = ItemCollect.GetComponent<Tooltip>();
			account.UseItem(component.item.itemUUID);
			TDGAItem.OnUse(string.Concat(component.item.itemID), 1);
		}
	}

	public void reLogin()
	{
		Object.Destroy((Object)(object)nowAvaterObj);
		PlayerPrefs.SetString("name", "");
		PlayerPrefs.SetString("password", "");
		SceneManager.UnloadScene("homeScene");
		SceneManager.LoadSceneAsync("login", (LoadSceneMode)1);
	}

	private void OnDestroy()
	{
		instense = null;
		Event.deregisterOut(this);
		Event.deregisterOut(friendmgr);
	}

	private void setUIErrorMsgNotActive()
	{
		UIErrorMsg.SetActive(false);
	}

	public void closeInspector()
	{
		ItemInspector.SetActive(false);
	}

	public void closeCollectInspector()
	{
		ItemCollect.SetActive(false);
	}

	public void closeUI()
	{
		ShopUI.SetActive(false);
		CollectUI.SetActive(false);
		camera.SetActive(false);
		ItemInspector.SetActive(false);
		buttonUI.SetActive(true);
	}

	private void Update()
	{
		List<ITEM_INFO> values = ((Account)KBEngineApp.app.player()).itemList.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO iTEM_INFO = values[i];
			if (iTEM_INFO.itemId == 0)
			{
				((Component)((Component)UserGold).transform.Find("Button").GetChild(0)).GetComponent<Text>().text = string.Concat(Convert.ToInt32(iTEM_INFO.itemCount));
			}
			if (iTEM_INFO.itemId == 1)
			{
				((Component)((Component)UserJade).transform.Find("Button").GetChild(0)).GetComponent<Text>().text = string.Concat(Convert.ToInt32(iTEM_INFO.itemCount));
			}
		}
	}

	public void onstartGame()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			World.instance.init();
			ulong teamuuid = TeamUI.GetComponent<TeamInfo>().teamuuid;
			if (teamuuid == 0L)
			{
				account.startMatch();
			}
			else
			{
				account.TeamStartMatch(teamuuid);
			}
		}
	}

	public void onShop(int shopID)
	{
		buttonUI.SetActive(false);
		camera.SetActive(true);
		ShopUI.SetActive(true);
		((Account)KBEngineApp.app.player())?.getShopList((uint)shopID);
	}

	public void onShopSoul()
	{
		onShop(1);
	}

	public void onShopBox()
	{
		onShop(2);
	}

	public void onShopHero()
	{
		onShop(3);
	}

	public void onShopHeroFace()
	{
		onShop(4);
	}

	public void onShopOther()
	{
		onShop(5);
	}

	public void buyShopItem()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			Tooltip component = ItemInspector.GetComponent<Tooltip>();
			account.buyShopItem((uint)component.item.itemUUID);
		}
	}

	public void createOder(string info)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		((AndroidJavaObject)new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity").Call<string>("ToAliPay", new object[1] { info });
	}

	public void PayResult()
	{
	}

	public bool onWillPay(string ticketId)
	{
		MonoBehaviour.print((object)("LGMGGAME::onWillPay" + ticketId));
		return false;
	}

	public void openCollect()
	{
		buttonUI.SetActive(false);
		CollectUI.SetActive(true);
		camera.SetActive(true);
		_ = (Account)KBEngineApp.app.player();
		Event.fireOut("openCollect");
	}

	public void SetMainBackgroudAvater(int _nowAvater = 0, int _nowAvaterSurface = 0)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Avater/Avater" + _nowAvater + "/Avater" + _nowAvater + "_" + _nowAvaterSurface);
		nowAvaterObj = Object.Instantiate<GameObject>(val, new Vector3(0.4f, -1.2f, -8f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
		Transform transform = nowAvaterObj.transform;
		transform.localScale *= 0.7f;
		Object.Destroy((Object)(object)nowAvaterObj.gameObject.GetComponent<CharacterController>());
	}
}
