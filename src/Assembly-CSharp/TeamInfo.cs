using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfo : MonoBehaviour
{
	public ulong teamuuid;

	public UI_HOMESCENE mainUI;

	private GameObject teamUI;

	private GameObject teamTemplate;

	private GameObject teamCraftingList;

	private GameObject ChoiceUI;

	private void Awake()
	{
		Event.registerOut("setTeamMember", this, "setTeamMember");
		Event.registerOut("setAllTeamMember", this, "setAllTeamMember");
		Event.registerOut("receiveaddTeam", this, "receiveaddTeam");
	}

	private void Start()
	{
		mainUI = UI_HOMESCENE.instense;
		teamUI = mainUI.TeamUI;
		ChoiceUI = mainUI.ChoiceUI;
		teamCraftingList = ((Component)mainUI.TeamUI.transform.Find("Scroll View").Find("Viewport").Find("Crafting List")).gameObject;
		teamTemplate = ((Component)teamCraftingList.transform.Find("Recipe (Template)")).gameObject;
		teamTemplate.SetActive(false);
	}

	public void setTeamMember(string name, uint LV)
	{
		mainUI.TeamUI.SetActive(true);
		GameObject obj = Object.Instantiate<GameObject>(teamTemplate.gameObject);
		obj.SetActive(true);
		obj.transform.parent = teamCraftingList.gameObject.transform;
		((Component)obj.transform.Find("name")).GetComponent<Text>().text = name;
		((Component)obj.transform.Find("LV")).GetComponent<Text>().text = "等级:" + (int)LV;
	}

	public void setAllTeamMember(string jsoninfo, ulong teamuuid)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		foreach (Transform item in teamCraftingList.gameObject.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.active)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		JSONObject jSONObject = new JSONObject(jsoninfo);
		for (int i = 0; i < jSONObject.list.Count; i++)
		{
			JSONObject jSONObject2 = jSONObject.list[i];
			mainUI.TeamUI.GetComponent<TeamInfo>().teamuuid = teamuuid;
			setTeamMember(jSONObject2["Name"].str, (uint)jSONObject2["LV"].n);
		}
	}

	public void receiveaddTeam(string friendname, ulong teamuuid, ulong frienddbid)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(ChoiceUI.gameObject);
		obj.gameObject.SetActive(true);
		obj.gameObject.AddComponent<ReceiveJoineam>();
		ReceiveJoineam component = obj.GetComponent<ReceiveJoineam>();
		component.teamUUID = teamuuid;
		component.friendName = friendname;
		component.friendDBid = frienddbid;
		((Component)obj.transform.Find("Text")).GetComponent<Text>().text = friendname + "邀请你一起游戏";
		obj.transform.parent = mainUI.UICavase.transform;
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	public void creatTeam(ulong friendDbid)
	{
		((Account)KBEngineApp.app.player())?.createTeam(friendDbid);
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void requestJoinTeam(ulong friendDbid)
	{
		((Account)KBEngineApp.app.player())?.requestJoinTeam(teamuuid, friendDbid);
	}
}
