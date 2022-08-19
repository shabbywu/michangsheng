using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029C RID: 668
public class CySelectWord : MonoBehaviour
{
	// Token: 0x060017F6 RID: 6134 RVA: 0x000A738A File Offset: 0x000A558A
	public void Init(string msg, int id)
	{
		this.msg.text = msg;
		this.questionId = id;
		base.gameObject.SetActive(true);
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x000A73AC File Offset: 0x000A55AC
	public void Say(object obj = null)
	{
		if (this.player == null)
		{
			this.player = Tools.instance.getPlayer();
		}
		if (CyUIMag.inst.npcList.curSelectFriend != null)
		{
			this.player.emailDateMag.SendToNpc(this.questionId, CyUIMag.inst.npcList.curSelectFriend.npcId, this.player.worldTimeMag.nowTime, obj);
			CyUIMag.inst.cyEmail.SendMsgCallBack();
		}
	}

	// Token: 0x040012D5 RID: 4821
	[SerializeField]
	private Text msg;

	// Token: 0x040012D6 RID: 4822
	public int questionId;

	// Token: 0x040012D7 RID: 4823
	public BtnCell btnCell;

	// Token: 0x040012D8 RID: 4824
	private Avatar player;

	// Token: 0x040012D9 RID: 4825
	public GameObject ChildPanel;

	// Token: 0x040012DA RID: 4826
	public GameObject ChildSelect;
}
