using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020005BE RID: 1470
public class UI_HOMESCENE : MonoBehaviour
{
	// Token: 0x0600250E RID: 9486 RVA: 0x0012A038 File Offset: 0x00128238
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

	// Token: 0x0600250F RID: 9487 RVA: 0x0012A108 File Offset: 0x00128308
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

	// Token: 0x06002510 RID: 9488 RVA: 0x000042DD File Offset: 0x000024DD
	public void showChiceUI()
	{
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x0012A3C0 File Offset: 0x001285C0
	public void addfriend()
	{
		Account account = (Account)KBEngineApp.app.player();
		string text = this.FriendUI.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
		if (account != null && text != "")
		{
			account.addFriend(text);
		}
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
	public void OpenPlayerInfoUI()
	{
		this.SetUI.SetActive(true);
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x0001DC02 File Offset: 0x0001BE02
	public void OpenFriendUI()
	{
		this.friendmgr.OpenFriendUI();
		this.FriendUI.SetActive(true);
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x0001DC1B File Offset: 0x0001BE1B
	public void closeFrendUI()
	{
		this.FriendUI.SetActive(false);
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x0001DC29 File Offset: 0x0001BE29
	public void closeTalkingUI()
	{
		this.talkingUI.SetActive(false);
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x0001DC37 File Offset: 0x0001BE37
	public void closePlayerInfoUI()
	{
		this.SetUI.SetActive(false);
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x0001DC45 File Offset: 0x0001BE45
	public void HomeErrorMessage(string msg)
	{
		UIPopTip.Inst.Pop(msg, PopTipIconType.叹号);
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x0012A420 File Offset: 0x00128620
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

	// Token: 0x06002519 RID: 9497 RVA: 0x0012A478 File Offset: 0x00128678
	public void reLogin()
	{
		Object.Destroy(this.nowAvaterObj);
		PlayerPrefs.SetString("name", "");
		PlayerPrefs.SetString("password", "");
		SceneManager.UnloadScene("homeScene");
		SceneManager.LoadSceneAsync("login", 1);
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x0001DC53 File Offset: 0x0001BE53
	private void OnDestroy()
	{
		UI_HOMESCENE.instense = null;
		Event.deregisterOut(this);
		Event.deregisterOut(this.friendmgr);
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x0001DC6E File Offset: 0x0001BE6E
	private void setUIErrorMsgNotActive()
	{
		this.UIErrorMsg.SetActive(false);
	}

	// Token: 0x0600251C RID: 9500 RVA: 0x0001DC7C File Offset: 0x0001BE7C
	public void closeInspector()
	{
		this.ItemInspector.SetActive(false);
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x0001DC8A File Offset: 0x0001BE8A
	public void closeCollectInspector()
	{
		this.ItemCollect.SetActive(false);
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x0001DC98 File Offset: 0x0001BE98
	public void closeUI()
	{
		this.ShopUI.SetActive(false);
		this.CollectUI.SetActive(false);
		this.camera.SetActive(false);
		this.ItemInspector.SetActive(false);
		this.buttonUI.SetActive(true);
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x0012A4C8 File Offset: 0x001286C8
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

	// Token: 0x06002520 RID: 9504 RVA: 0x0012A594 File Offset: 0x00128794
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

	// Token: 0x06002521 RID: 9505 RVA: 0x0012A5E0 File Offset: 0x001287E0
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

	// Token: 0x06002522 RID: 9506 RVA: 0x0001DCD6 File Offset: 0x0001BED6
	public void onShopSoul()
	{
		this.onShop(1);
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x0001DCDF File Offset: 0x0001BEDF
	public void onShopBox()
	{
		this.onShop(2);
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
	public void onShopHero()
	{
		this.onShop(3);
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x0001DCF1 File Offset: 0x0001BEF1
	public void onShopHeroFace()
	{
		this.onShop(4);
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x0001DCFA File Offset: 0x0001BEFA
	public void onShopOther()
	{
		this.onShop(5);
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x0012A62C File Offset: 0x0012882C
	public void buyShopItem()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			Tooltip component = this.ItemInspector.GetComponent<Tooltip>();
			account.buyShopItem((ulong)((uint)component.item.itemUUID));
		}
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x0001DD03 File Offset: 0x0001BF03
	public void createOder(string info)
	{
		new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<string>("ToAliPay", new object[]
		{
			info
		});
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x000042DD File Offset: 0x000024DD
	public void PayResult()
	{
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x0001DD2E File Offset: 0x0001BF2E
	public bool onWillPay(string ticketId)
	{
		MonoBehaviour.print("LGMGGAME::onWillPay" + ticketId);
		return false;
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x0012A66C File Offset: 0x0012886C
	public void openCollect()
	{
		this.buttonUI.SetActive(false);
		this.CollectUI.SetActive(true);
		this.camera.SetActive(true);
		Account account = (Account)KBEngineApp.app.player();
		Event.fireOut("openCollect", Array.Empty<object>());
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x0012A6BC File Offset: 0x001288BC
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

	// Token: 0x04001FA8 RID: 8104
	public Text UserName;

	// Token: 0x04001FA9 RID: 8105
	public Text UserGold;

	// Token: 0x04001FAA RID: 8106
	public Text UserJade;

	// Token: 0x04001FAB RID: 8107
	private string TDpayOder;

	// Token: 0x04001FAC RID: 8108
	public ItemDatabase database;

	// Token: 0x04001FAD RID: 8109
	public GameObject ItemInspector;

	// Token: 0x04001FAE RID: 8110
	public GameObject ItemCollect;

	// Token: 0x04001FAF RID: 8111
	public GameObject ItemCheckIn;

	// Token: 0x04001FB0 RID: 8112
	public GameObject camera;

	// Token: 0x04001FB1 RID: 8113
	public GameObject buttonUI;

	// Token: 0x04001FB2 RID: 8114
	public GameObject UICavase;

	// Token: 0x04001FB3 RID: 8115
	public GameObject ShopUI;

	// Token: 0x04001FB4 RID: 8116
	public GameObject CollectUI;

	// Token: 0x04001FB5 RID: 8117
	public GameObject UIErrorMsg;

	// Token: 0x04001FB6 RID: 8118
	public GameObject SetUI;

	// Token: 0x04001FB7 RID: 8119
	public GameObject talkingUI;

	// Token: 0x04001FB8 RID: 8120
	public GameObject FriendUI;

	// Token: 0x04001FB9 RID: 8121
	public GameObject ChoiceUI;

	// Token: 0x04001FBA RID: 8122
	public GameObject TeamUI;

	// Token: 0x04001FBB RID: 8123
	private GameObject nowAvaterObj;

	// Token: 0x04001FBC RID: 8124
	public static UI_HOMESCENE instense;

	// Token: 0x04001FBD RID: 8125
	private FriendMgr friendmgr;
}
