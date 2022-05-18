using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000575 RID: 1397
public class FriendMgr
{
	// Token: 0x0600237B RID: 9083 RVA: 0x00124148 File Offset: 0x00122348
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

	// Token: 0x0600237C RID: 9084 RVA: 0x00124274 File Offset: 0x00122474
	~FriendMgr()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x001242A4 File Offset: 0x001224A4
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

	// Token: 0x0600237E RID: 9086 RVA: 0x0012434C File Offset: 0x0012254C
	public void OpenFriendUI()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.getOnlineFriend();
		}
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x00124374 File Offset: 0x00122574
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

	// Token: 0x06002380 RID: 9088 RVA: 0x00124404 File Offset: 0x00122604
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

	// Token: 0x06002381 RID: 9089 RVA: 0x00124594 File Offset: 0x00122794
	public void getTalkingMsg(FRIEND_INFO Info, string msg)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.talkingTemplate.gameObject);
		gameObject.SetActive(true);
		gameObject.transform.Find("Text").GetComponent<Text>().text = Info.name + ":" + msg;
		gameObject.transform.parent = this.talkingsCraftingList.gameObject.transform;
	}

	// Token: 0x04001E8E RID: 7822
	private UI_HOMESCENE mainHomeUI;

	// Token: 0x04001E8F RID: 7823
	private GameObject FriendCraftingList;

	// Token: 0x04001E90 RID: 7824
	private GameObject FriendTemplate;

	// Token: 0x04001E91 RID: 7825
	private GameObject ChoiceUI;

	// Token: 0x04001E92 RID: 7826
	private List<GameObject> receiveadd;

	// Token: 0x04001E93 RID: 7827
	private GameObject talkingTemplate;

	// Token: 0x04001E94 RID: 7828
	private GameObject talkingsCraftingList;
}
