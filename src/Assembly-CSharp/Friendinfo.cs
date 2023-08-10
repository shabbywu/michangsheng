using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Friendinfo : MonoBehaviour
{
	public ulong friendDbid;

	public string friendName;

	public int friendLeval;

	private void Start()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		((UnityEvent)((Component)((Component)this).transform.Find("talk")).GetComponent<Button>().onClick).AddListener(new UnityAction(OpenTalkingUI));
		((UnityEvent)((Component)((Component)this).transform.Find("join")).GetComponent<Button>().onClick).AddListener(new UnityAction(receiveFriend));
	}

	public void OpenTalkingUI()
	{
		GameObject talkingUI = UI_HOMESCENE.instense.talkingUI;
		talkingUI.SetActive(true);
		talkingUI.GetComponent<FriendTalkingInfo>().friendDbid = friendDbid;
		talkingUI.GetComponent<FriendTalkingInfo>().friendName = friendName;
	}

	public void receiveFriend()
	{
		_ = (Account)KBEngineApp.app.player();
		TeamInfo component = UI_HOMESCENE.instense.TeamUI.GetComponent<TeamInfo>();
		if (component.teamuuid == 0L)
		{
			component.creatTeam(friendDbid);
		}
		else
		{
			component.requestJoinTeam(friendDbid);
		}
	}
}
