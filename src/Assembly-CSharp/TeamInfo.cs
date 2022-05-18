using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005AD RID: 1453
public class TeamInfo : MonoBehaviour
{
	// Token: 0x0600249F RID: 9375 RVA: 0x0001D6CB File Offset: 0x0001B8CB
	private void Awake()
	{
		Event.registerOut("setTeamMember", this, "setTeamMember");
		Event.registerOut("setAllTeamMember", this, "setAllTeamMember");
		Event.registerOut("receiveaddTeam", this, "receiveaddTeam");
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x00129264 File Offset: 0x00127464
	private void Start()
	{
		this.mainUI = UI_HOMESCENE.instense;
		this.teamUI = this.mainUI.TeamUI;
		this.ChoiceUI = this.mainUI.ChoiceUI;
		this.teamCraftingList = this.mainUI.TeamUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List").gameObject;
		this.teamTemplate = this.teamCraftingList.transform.Find("Recipe (Template)").gameObject;
		this.teamTemplate.SetActive(false);
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x00129304 File Offset: 0x00127504
	public void setTeamMember(string name, uint LV)
	{
		this.mainUI.TeamUI.SetActive(true);
		GameObject gameObject = Object.Instantiate<GameObject>(this.teamTemplate.gameObject);
		gameObject.SetActive(true);
		gameObject.transform.parent = this.teamCraftingList.gameObject.transform;
		gameObject.transform.Find("name").GetComponent<Text>().text = name;
		gameObject.transform.Find("LV").GetComponent<Text>().text = "等级:" + (int)LV;
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x00129398 File Offset: 0x00127598
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

	// Token: 0x060024A3 RID: 9379 RVA: 0x00129480 File Offset: 0x00127680
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

	// Token: 0x060024A4 RID: 9380 RVA: 0x00129530 File Offset: 0x00127730
	public void creatTeam(ulong friendDbid)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.createTeam(friendDbid);
		}
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x00129558 File Offset: 0x00127758
	public void requestJoinTeam(ulong friendDbid)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.requestJoinTeam(this.teamuuid, friendDbid);
		}
	}

	// Token: 0x04001F72 RID: 8050
	public ulong teamuuid;

	// Token: 0x04001F73 RID: 8051
	public UI_HOMESCENE mainUI;

	// Token: 0x04001F74 RID: 8052
	private GameObject teamUI;

	// Token: 0x04001F75 RID: 8053
	private GameObject teamTemplate;

	// Token: 0x04001F76 RID: 8054
	private GameObject teamCraftingList;

	// Token: 0x04001F77 RID: 8055
	private GameObject ChoiceUI;
}
