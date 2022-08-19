using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003FD RID: 1021
public class TeamInfo : MonoBehaviour
{
	// Token: 0x060020ED RID: 8429 RVA: 0x000E729C File Offset: 0x000E549C
	private void Awake()
	{
		Event.registerOut("setTeamMember", this, "setTeamMember");
		Event.registerOut("setAllTeamMember", this, "setAllTeamMember");
		Event.registerOut("receiveaddTeam", this, "receiveaddTeam");
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x000E72D4 File Offset: 0x000E54D4
	private void Start()
	{
		this.mainUI = UI_HOMESCENE.instense;
		this.teamUI = this.mainUI.TeamUI;
		this.ChoiceUI = this.mainUI.ChoiceUI;
		this.teamCraftingList = this.mainUI.TeamUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List").gameObject;
		this.teamTemplate = this.teamCraftingList.transform.Find("Recipe (Template)").gameObject;
		this.teamTemplate.SetActive(false);
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x000E7374 File Offset: 0x000E5574
	public void setTeamMember(string name, uint LV)
	{
		this.mainUI.TeamUI.SetActive(true);
		GameObject gameObject = Object.Instantiate<GameObject>(this.teamTemplate.gameObject);
		gameObject.SetActive(true);
		gameObject.transform.parent = this.teamCraftingList.gameObject.transform;
		gameObject.transform.Find("name").GetComponent<Text>().text = name;
		gameObject.transform.Find("LV").GetComponent<Text>().text = "等级:" + (int)LV;
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000E7408 File Offset: 0x000E5608
	public void setAllTeamMember(string jsoninfo, ulong teamuuid)
	{
		foreach (object obj in this.teamCraftingList.gameObject.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.active)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		JSONObject jsonobject = new JSONObject(jsoninfo, -2, false, false);
		for (int i = 0; i < jsonobject.list.Count; i++)
		{
			JSONObject jsonobject2 = jsonobject.list[i];
			this.mainUI.TeamUI.GetComponent<TeamInfo>().teamuuid = teamuuid;
			this.setTeamMember(jsonobject2["Name"].str, (uint)jsonobject2["LV"].n);
		}
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x000E74F0 File Offset: 0x000E56F0
	public void receiveaddTeam(string friendname, ulong teamuuid, ulong frienddbid)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.ChoiceUI.gameObject);
		gameObject.gameObject.SetActive(true);
		gameObject.gameObject.AddComponent<ReceiveJoineam>();
		ReceiveJoineam component = gameObject.GetComponent<ReceiveJoineam>();
		component.teamUUID = teamuuid;
		component.friendName = friendname;
		component.friendDBid = frienddbid;
		gameObject.transform.Find("Text").GetComponent<Text>().text = friendname + "邀请你一起游戏";
		gameObject.transform.parent = this.mainUI.UICavase.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x000E75A0 File Offset: 0x000E57A0
	public void creatTeam(ulong friendDbid)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.createTeam(friendDbid);
		}
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x000E75C8 File Offset: 0x000E57C8
	public void requestJoinTeam(ulong friendDbid)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.requestJoinTeam(this.teamuuid, friendDbid);
		}
	}

	// Token: 0x04001AB6 RID: 6838
	public ulong teamuuid;

	// Token: 0x04001AB7 RID: 6839
	public UI_HOMESCENE mainUI;

	// Token: 0x04001AB8 RID: 6840
	private GameObject teamUI;

	// Token: 0x04001AB9 RID: 6841
	private GameObject teamTemplate;

	// Token: 0x04001ABA RID: 6842
	private GameObject teamCraftingList;

	// Token: 0x04001ABB RID: 6843
	private GameObject ChoiceUI;
}
