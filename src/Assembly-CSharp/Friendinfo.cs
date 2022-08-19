using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003D9 RID: 985
public class Friendinfo : MonoBehaviour
{
	// Token: 0x06001FF7 RID: 8183 RVA: 0x000E1620 File Offset: 0x000DF820
	private void Start()
	{
		base.transform.Find("talk").GetComponent<Button>().onClick.AddListener(new UnityAction(this.OpenTalkingUI));
		base.transform.Find("join").GetComponent<Button>().onClick.AddListener(new UnityAction(this.receiveFriend));
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x000E1683 File Offset: 0x000DF883
	public void OpenTalkingUI()
	{
		GameObject talkingUI = UI_HOMESCENE.instense.talkingUI;
		talkingUI.SetActive(true);
		talkingUI.GetComponent<FriendTalkingInfo>().friendDbid = this.friendDbid;
		talkingUI.GetComponent<FriendTalkingInfo>().friendName = this.friendName;
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x000E16B8 File Offset: 0x000DF8B8
	public void receiveFriend()
	{
		Account account = (Account)KBEngineApp.app.player();
		TeamInfo component = UI_HOMESCENE.instense.TeamUI.GetComponent<TeamInfo>();
		if (component.teamuuid == 0UL)
		{
			component.creatTeam(this.friendDbid);
			return;
		}
		component.requestJoinTeam(this.friendDbid);
	}

	// Token: 0x040019F9 RID: 6649
	public ulong friendDbid;

	// Token: 0x040019FA RID: 6650
	public string friendName;

	// Token: 0x040019FB RID: 6651
	public int friendLeval;
}
