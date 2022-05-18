using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000574 RID: 1396
public class Friendinfo : MonoBehaviour
{
	// Token: 0x06002377 RID: 9079 RVA: 0x00124094 File Offset: 0x00122294
	private void Start()
	{
		base.transform.Find("talk").GetComponent<Button>().onClick.AddListener(new UnityAction(this.OpenTalkingUI));
		base.transform.Find("join").GetComponent<Button>().onClick.AddListener(new UnityAction(this.receiveFriend));
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x0001CAD2 File Offset: 0x0001ACD2
	public void OpenTalkingUI()
	{
		GameObject talkingUI = UI_HOMESCENE.instense.talkingUI;
		talkingUI.SetActive(true);
		talkingUI.GetComponent<FriendTalkingInfo>().friendDbid = this.friendDbid;
		talkingUI.GetComponent<FriendTalkingInfo>().friendName = this.friendName;
	}

	// Token: 0x06002379 RID: 9081 RVA: 0x001240F8 File Offset: 0x001222F8
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

	// Token: 0x04001E8B RID: 7819
	public ulong friendDbid;

	// Token: 0x04001E8C RID: 7820
	public string friendName;

	// Token: 0x04001E8D RID: 7821
	public int friendLeval;
}
