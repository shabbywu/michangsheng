using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class FriendMgr
{
	private UI_HOMESCENE mainHomeUI;

	private GameObject FriendCraftingList;

	private GameObject FriendTemplate;

	private GameObject ChoiceUI;

	private List<GameObject> receiveadd;

	private GameObject talkingTemplate;

	private GameObject talkingsCraftingList;

	public FriendMgr()
	{
		mainHomeUI = UI_HOMESCENE.instense;
		ChoiceUI = mainHomeUI.ChoiceUI;
		FriendCraftingList = ((Component)mainHomeUI.FriendUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List")).gameObject;
		FriendTemplate = ((Component)FriendCraftingList.transform.Find("Recipe (Template)")).gameObject;
		FriendTemplate.SetActive(false);
		talkingsCraftingList = ((Component)mainHomeUI.talkingUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List")).gameObject;
		talkingTemplate = ((Component)talkingsCraftingList.transform.Find("Recipe (Template)")).gameObject;
		talkingTemplate.SetActive(false);
		Event.registerOut("requestOnlineFriend", this, "requestOnlineFriend");
		Event.registerOut("getTalkingMsg", this, "getTalkingMsg");
		Event.registerOut("receiveaddfriend", this, "receiveaddfriend");
	}

	~FriendMgr()
	{
		Event.deregisterOut(this);
	}

	public void receiveaddfriend(string friendname, ulong frienddbid)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(ChoiceUI.gameObject);
		obj.gameObject.SetActive(true);
		obj.gameObject.AddComponent<ReceiveFriend>();
		ReceiveFriend component = obj.GetComponent<ReceiveFriend>();
		component.friendDbid = frienddbid;
		component.friendName = friendname;
		((Component)obj.transform.Find("Text")).GetComponent<Text>().text = friendname + "请求加你为好友";
		obj.transform.parent = mainHomeUI.UICavase.transform;
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	public void OpenFriendUI()
	{
		((Account)KBEngineApp.app.player())?.getOnlineFriend();
	}

	public void requestOnlineFriend(FRIEND_INFO_LIST infos)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		foreach (Transform item in FriendCraftingList.gameObject.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.active)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			addfriendlistdo(infos, account);
		}
	}

	private void addfriendlistdo(FRIEND_INFO_LIST infos, Account account)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		List<FRIEND_INFO> values = account.FriendList.values;
		List<FRIEND_INFO> values2 = infos.values;
		for (int i = 0; i < values.Count; i++)
		{
			FRIEND_INFO fRIEND_INFO = values[i];
			GameObject val = Object.Instantiate<GameObject>(FriendTemplate.gameObject);
			val.transform.parent = FriendCraftingList.gameObject.transform;
			val.transform.localScale = new Vector3(1f, 1f, 1f);
			Friendinfo component = val.GetComponent<Friendinfo>();
			val.SetActive(true);
			component.friendDbid = fRIEND_INFO.dbid;
			component.friendName = fRIEND_INFO.name;
			component.friendLeval = (int)fRIEND_INFO.level;
			((Component)((Component)component).transform.Find("Text")).GetComponent<Text>().text = component.friendName;
			((Component)((Component)component).transform.Find("join")).gameObject.SetActive(false);
			((Component)((Component)component).transform.Find("talk")).gameObject.SetActive(false);
			for (int j = 0; j < values2.Count; j++)
			{
				if (values2[j].dbid == component.friendDbid)
				{
					((Component)((Component)component).transform.Find("join")).gameObject.SetActive(true);
					((Component)((Component)component).transform.Find("talk")).gameObject.SetActive(true);
					val.transform.SetSiblingIndex(0);
				}
			}
		}
	}

	public void getTalkingMsg(FRIEND_INFO Info, string msg)
	{
		GameObject obj = Object.Instantiate<GameObject>(talkingTemplate.gameObject);
		obj.SetActive(true);
		((Component)obj.transform.Find("Text")).GetComponent<Text>().text = Info.name + ":" + msg;
		obj.transform.parent = talkingsCraftingList.gameObject.transform;
	}
}
