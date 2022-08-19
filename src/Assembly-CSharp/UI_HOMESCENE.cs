using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200040C RID: 1036
public class UI_HOMESCENE : MonoBehaviour
{
	// Token: 0x06002156 RID: 8534 RVA: 0x000E854C File Offset: 0x000E674C
	private void Start()
	{
		UI_HOMESCENE.instense = this;
		this.initUICavase();
		Event.registerOut("HomeErrorMessage", this, "HomeErrorMessage");
		Event.registerOut("createOder", this, "createOder");
		Event.registerOut("OpenFriendUI", this, "OpenFriendUI");
		Event.registerOut("showChiceUI", this, "showChiceUI");
		this.ShopUI.SetActive(false);
		this.CollectUI.SetActive(false);
		Random random = new Random();
		int num = 50 + random.Next() % 5;
		if (num == 52)
		{
			this.SetMainBackgroudAvater(53, 3);
		}
		else
		{
			this.SetMainBackgroudAvater(num, 3);
		}
		this.database = (ItemDatabase)Resources.Load("Custom Data/Items/PlayerItem Database");
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.onPayEnd();
		}
	}

	// Token: 0x06002157 RID: 8535 RVA: 0x000E861C File Offset: 0x000E681C
	public void initUICavase()
	{
		AccountBase accountBase = (Account)KBEngineApp.app.player();
		Transform transform = this.UICavase.transform.Find("ShopUI");
		Transform transform2 = this.UICavase.transform.Find("CollectUI");
		Object @object = this.UICavase.transform.Find("Text_error");
		Transform transform3 = this.UICavase.transform.Find("setUI");
		Transform transform4 = this.UICavase.transform.Find("talkingUI");
		Transform transform5 = this.UICavase.transform.Find("friendUI");
		Transform transform6 = this.UICavase.transform.Find("choiceUI");
		Transform transform7 = this.UICavase.transform.Find("TeamUI");
		if (transform != null)
		{
			this.ShopUI = transform.gameObject;
		}
		if (transform2 != null)
		{
			this.CollectUI = transform2.gameObject;
		}
		if (@object != null)
		{
			this.SetUI = transform3.gameObject;
		}
		if (transform4 != null)
		{
			this.talkingUI = transform4.gameObject;
		}
		if (transform5 != null)
		{
			this.FriendUI = transform5.gameObject;
		}
		if (transform6 != null)
		{
			this.ChoiceUI = transform6.gameObject;
		}
		if (transform6 != null)
		{
			this.TeamUI = transform7.gameObject;
		}
		PLAYER_INFO playerInfo = accountBase.playerInfo;
		Transform transform8 = this.SetUI.transform.Find("ItemSet");
		transform8.Find("name").GetComponent<Text>().text = playerInfo.name;
		transform8.Find("Leave").GetComponent<Text>().text = "等级:" + playerInfo.level;
		float exp = (float)playerInfo.Exp;
		this.UserName.transform.Find("Button").GetChild(0).GetComponent<Text>().text = (playerInfo.name ?? "");
		float num = exp;
		transform8.Find("Panel_target").Find("Slider_targetExp").GetComponent<Slider>().value = num;
		transform8.Find("Panel_target").Find("Text_exp").GetComponent<Text>().text = num + "/1000";
		this.CollectUI.SetActive(false);
		this.ShopUI.SetActive(false);
		this.SetUI.SetActive(false);
		this.talkingUI.SetActive(false);
		this.FriendUI.SetActive(false);
		this.ChoiceUI.SetActive(false);
		this.TeamUI.SetActive(true);
		this.friendmgr = new FriendMgr();
	}

	// Token: 0x06002158 RID: 8536 RVA: 0x00004095 File Offset: 0x00002295
	public void showChiceUI()
	{
	}

	// Token: 0x06002159 RID: 8537 RVA: 0x000E88D4 File Offset: 0x000E6AD4
	public void addfriend()
	{
		Account account = (Account)KBEngineApp.app.player();
		string text = this.FriendUI.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
		if (account != null && text != "")
		{
			account.addFriend(text);
		}
	}

	// Token: 0x0600215A RID: 8538 RVA: 0x000E8932 File Offset: 0x000E6B32
	public void OpenPlayerInfoUI()
	{
		this.SetUI.SetActive(true);
	}

	// Token: 0x0600215B RID: 8539 RVA: 0x000E8940 File Offset: 0x000E6B40
	public void OpenFriendUI()
	{
		this.friendmgr.OpenFriendUI();
		this.FriendUI.SetActive(true);
	}

	// Token: 0x0600215C RID: 8540 RVA: 0x000E8959 File Offset: 0x000E6B59
	public void closeFrendUI()
	{
		this.FriendUI.SetActive(false);
	}

	// Token: 0x0600215D RID: 8541 RVA: 0x000E8967 File Offset: 0x000E6B67
	public void closeTalkingUI()
	{
		this.talkingUI.SetActive(false);
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x000E8975 File Offset: 0x000E6B75
	public void closePlayerInfoUI()
	{
		this.SetUI.SetActive(false);
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x000E8983 File Offset: 0x000E6B83
	public void HomeErrorMessage(string msg)
	{
		UIPopTip.Inst.Pop(msg, PopTipIconType.叹号);
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x000E8994 File Offset: 0x000E6B94
	public void useCollect()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			Tooltip component = this.ItemCollect.GetComponent<Tooltip>();
			account.UseItem(component.item.itemUUID);
			TDGAItem.OnUse(string.Concat(component.item.itemID), 1);
		}
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x000E89EC File Offset: 0x000E6BEC
	public void reLogin()
	{
		Object.Destroy(this.nowAvaterObj);
		PlayerPrefs.SetString("name", "");
		PlayerPrefs.SetString("password", "");
		SceneManager.UnloadScene("homeScene");
		SceneManager.LoadSceneAsync("login", 1);
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x000E8A39 File Offset: 0x000E6C39
	private void OnDestroy()
	{
		UI_HOMESCENE.instense = null;
		Event.deregisterOut(this);
		Event.deregisterOut(this.friendmgr);
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x000E8A54 File Offset: 0x000E6C54
	private void setUIErrorMsgNotActive()
	{
		this.UIErrorMsg.SetActive(false);
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x000E8A62 File Offset: 0x000E6C62
	public void closeInspector()
	{
		this.ItemInspector.SetActive(false);
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x000E8A70 File Offset: 0x000E6C70
	public void closeCollectInspector()
	{
		this.ItemCollect.SetActive(false);
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x000E8A7E File Offset: 0x000E6C7E
	public void closeUI()
	{
		this.ShopUI.SetActive(false);
		this.CollectUI.SetActive(false);
		this.camera.SetActive(false);
		this.ItemInspector.SetActive(false);
		this.buttonUI.SetActive(true);
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x000E8ABC File Offset: 0x000E6CBC
	private void Update()
	{
		List<ITEM_INFO> values = ((Account)KBEngineApp.app.player()).itemList.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO item_INFO = values[i];
			if (item_INFO.itemId == 0)
			{
				this.UserGold.transform.Find("Button").GetChild(0).GetComponent<Text>().text = string.Concat(Convert.ToInt32(item_INFO.itemCount));
			}
			if (item_INFO.itemId == 1)
			{
				this.UserJade.transform.Find("Button").GetChild(0).GetComponent<Text>().text = string.Concat(Convert.ToInt32(item_INFO.itemCount));
			}
		}
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x000E8B88 File Offset: 0x000E6D88
	public void onstartGame()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			World.instance.init();
			ulong teamuuid = this.TeamUI.GetComponent<TeamInfo>().teamuuid;
			if (teamuuid == 0UL)
			{
				account.startMatch();
				return;
			}
			account.TeamStartMatch(teamuuid);
		}
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x000E8BD4 File Offset: 0x000E6DD4
	public void onShop(int shopID)
	{
		this.buttonUI.SetActive(false);
		this.camera.SetActive(true);
		this.ShopUI.SetActive(true);
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.getShopList((uint)shopID);
		}
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x000E8C1F File Offset: 0x000E6E1F
	public void onShopSoul()
	{
		this.onShop(1);
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x000E8C28 File Offset: 0x000E6E28
	public void onShopBox()
	{
		this.onShop(2);
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x000E8C31 File Offset: 0x000E6E31
	public void onShopHero()
	{
		this.onShop(3);
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x000E8C3A File Offset: 0x000E6E3A
	public void onShopHeroFace()
	{
		this.onShop(4);
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x000E8C43 File Offset: 0x000E6E43
	public void onShopOther()
	{
		this.onShop(5);
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x000E8C4C File Offset: 0x000E6E4C
	public void buyShopItem()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			Tooltip component = this.ItemInspector.GetComponent<Tooltip>();
			account.buyShopItem((ulong)((uint)component.item.itemUUID));
		}
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x000E8C8B File Offset: 0x000E6E8B
	public void createOder(string info)
	{
		new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<string>("ToAliPay", new object[]
		{
			info
		});
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x00004095 File Offset: 0x00002295
	public void PayResult()
	{
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x000E8CB6 File Offset: 0x000E6EB6
	public bool onWillPay(string ticketId)
	{
		MonoBehaviour.print("LGMGGAME::onWillPay" + ticketId);
		return false;
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x000E8CCC File Offset: 0x000E6ECC
	public void openCollect()
	{
		this.buttonUI.SetActive(false);
		this.CollectUI.SetActive(true);
		this.camera.SetActive(true);
		Account account = (Account)KBEngineApp.app.player();
		Event.fireOut("openCollect", Array.Empty<object>());
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x000E8D1C File Offset: 0x000E6F1C
	public void SetMainBackgroudAvater(int _nowAvater = 0, int _nowAvaterSurface = 0)
	{
		GameObject gameObject = (GameObject)Resources.Load(string.Concat(new object[]
		{
			"Effect/Prefab/gameEntity/Avater/Avater",
			_nowAvater,
			"/Avater",
			_nowAvater,
			"_",
			_nowAvaterSurface
		}));
		this.nowAvaterObj = Object.Instantiate<GameObject>(gameObject, new Vector3(0.4f, -1.2f, -8f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
		this.nowAvaterObj.transform.localScale *= 0.7f;
		Object.Destroy(this.nowAvaterObj.gameObject.GetComponent<CharacterController>());
	}

	// Token: 0x04001AE9 RID: 6889
	public Text UserName;

	// Token: 0x04001AEA RID: 6890
	public Text UserGold;

	// Token: 0x04001AEB RID: 6891
	public Text UserJade;

	// Token: 0x04001AEC RID: 6892
	private string TDpayOder;

	// Token: 0x04001AED RID: 6893
	public ItemDatabase database;

	// Token: 0x04001AEE RID: 6894
	public GameObject ItemInspector;

	// Token: 0x04001AEF RID: 6895
	public GameObject ItemCollect;

	// Token: 0x04001AF0 RID: 6896
	public GameObject ItemCheckIn;

	// Token: 0x04001AF1 RID: 6897
	public GameObject camera;

	// Token: 0x04001AF2 RID: 6898
	public GameObject buttonUI;

	// Token: 0x04001AF3 RID: 6899
	public GameObject UICavase;

	// Token: 0x04001AF4 RID: 6900
	public GameObject ShopUI;

	// Token: 0x04001AF5 RID: 6901
	public GameObject CollectUI;

	// Token: 0x04001AF6 RID: 6902
	public GameObject UIErrorMsg;

	// Token: 0x04001AF7 RID: 6903
	public GameObject SetUI;

	// Token: 0x04001AF8 RID: 6904
	public GameObject talkingUI;

	// Token: 0x04001AF9 RID: 6905
	public GameObject FriendUI;

	// Token: 0x04001AFA RID: 6906
	public GameObject ChoiceUI;

	// Token: 0x04001AFB RID: 6907
	public GameObject TeamUI;

	// Token: 0x04001AFC RID: 6908
	private GameObject nowAvaterObj;

	// Token: 0x04001AFD RID: 6909
	public static UI_HOMESCENE instense;

	// Token: 0x04001AFE RID: 6910
	private FriendMgr friendmgr;
}
