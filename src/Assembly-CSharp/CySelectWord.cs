using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class CySelectWord : MonoBehaviour
{
	[SerializeField]
	private Text msg;

	public int questionId;

	public BtnCell btnCell;

	private Avatar player;

	public GameObject ChildPanel;

	public GameObject ChildSelect;

	public void Init(string msg, int id)
	{
		this.msg.text = msg;
		questionId = id;
		((Component)this).gameObject.SetActive(true);
	}

	public void Say(object obj = null)
	{
		if (player == null)
		{
			player = Tools.instance.getPlayer();
		}
		if ((Object)(object)CyUIMag.inst.npcList.curSelectFriend != (Object)null)
		{
			player.emailDateMag.SendToNpc(questionId, CyUIMag.inst.npcList.curSelectFriend.npcId, player.worldTimeMag.nowTime, obj);
			CyUIMag.inst.cyEmail.SendMsgCallBack();
		}
	}
}
