using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DA RID: 986
public class FriendMgr
{
	// Token: 0x06001FFB RID: 8187 RVA: 0x000E1708 File Offset: 0x000DF908
	public FriendMgr()
	{
		this.mainHomeUI = UI_HOMESCENE.instense;
		this.ChoiceUI = this.mainHomeUI.ChoiceUI;
		this.FriendCraftingList = this.mainHomeUI.FriendUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List").gameObject;
		this.FriendTemplate = this.FriendCraftingList.transform.Find("Recipe (Template)").gameObject;
		this.FriendTemplate.SetActive(false);
		this.talkingsCraftingList = this.mainHomeUI.talkingUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List").gameObject;
		this.talkingTemplate = this.talkingsCraftingList.transform.Find("Recipe (Template)").gameObject;
		this.talkingTemplate.SetActive(false);
		Event.registerOut("requestOnlineFriend", this, "requestOnlineFriend");
		Event.registerOut("getTalkingMsg", this, "getTalkingMsg");
		Event.registerOut("receiveaddfriend", this, "receiveaddfriend");
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x000E1834 File Offset: 0x000DFA34
	~FriendMgr()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x000E1864 File Offset: 0x000DFA64
	public void receiveaddfriend(string friendname, ulong frienddbid)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.ChoiceUI.gameObject);
		gameObject.gameObject.SetActive(true);
		gameObject.gameObject.AddComponent<ReceiveFriend>();
		ReceiveFriend component = gameObject.GetComponent<ReceiveFriend>();
		component.friendDbid = frienddbid;
		component.friendName = friendname;
		gameObject.transform.Find("Text").GetComponent<Text>().text = friendname + "请求加你为好友";
		gameObject.transform.parent = this.mainHomeUI.UICavase.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x000E190C File Offset: 0x000DFB0C
	public void OpenFriendUI()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.getOnlineFriend();
		}
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x000E1934 File Offset: 0x000DFB34
	public void requestOnlineFriend(FRIEND_INFO_LIST infos)
	{
		foreach (object obj in this.FriendCraftingList.gameObject.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.active)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			this.addfriendlistdo(infos, account);
		}
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x000E19C4 File Offset: 0x000DFBC4
	private void addfriendlistdo(FRIEND_INFO_LIST infos, Account account)
	{
		List<FRIEND_INFO> values = account.FriendList.values;
		List<FRIEND_INFO> values2 = infos.values;
		for (int i = 0; i < values.Count; i++)
		{
			FRIEND_INFO friend_INFO = values[i];
			GameObject gameObject = Object.Instantiate<GameObject>(this.FriendTemplate.gameObject);
			gameObject.transform.parent = this.FriendCraftingList.gameObject.transform;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			Friendinfo component = gameObject.GetComponent<Friendinfo>();
			gameObject.SetActive(true);
			component.friendDbid = friend_INFO.dbid;
			component.friendName = friend_INFO.name;
			component.friendLeval = (int)friend_INFO.level;
			component.transform.Find("Text").GetComponent<Text>().text = component.friendName;
			component.transform.Find("join").gameObject.SetActive(false);
			component.transform.Find("talk").gameObject.SetActive(false);
			for (int j = 0; j < values2.Count; j++)
			{
				if (values2[j].dbid == component.friendDbid)
				{
					component.transform.Find("join").gameObject.SetActive(true);
					component.transform.Find("talk").gameObject.SetActive(true);
					gameObject.transform.SetSiblingIndex(0);
				}
			}
		}
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x000E1B54 File Offset: 0x000DFD54
	public void getTalkingMsg(FRIEND_INFO Info, string msg)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.talkingTemplate.gameObject);
		gameObject.SetActive(true);
		gameObject.transform.Find("Text").GetComponent<Text>().text = Info.name + ":" + msg;
		gameObject.transform.parent = this.talkingsCraftingList.gameObject.transform;
	}

	// Token: 0x040019FC RID: 6652
	private UI_HOMESCENE mainHomeUI;

	// Token: 0x040019FD RID: 6653
	private GameObject FriendCraftingList;

	// Token: 0x040019FE RID: 6654
	private GameObject FriendTemplate;

	// Token: 0x040019FF RID: 6655
	private GameObject ChoiceUI;

	// Token: 0x04001A00 RID: 6656
	private List<GameObject> receiveadd;

	// Token: 0x04001A01 RID: 6657
	private GameObject talkingTemplate;

	// Token: 0x04001A02 RID: 6658
	private GameObject talkingsCraftingList;
}
