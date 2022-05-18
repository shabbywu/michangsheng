using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D0 RID: 976
public class CySelectWord : MonoBehaviour
{
	// Token: 0x06001ADE RID: 6878 RVA: 0x00016C6D File Offset: 0x00014E6D
	public void Init(string msg, int id)
	{
		this.msg.text = msg;
		this.questionId = id;
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x000EE4E4 File Offset: 0x000EC6E4
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

	// Token: 0x04001666 RID: 5734
	[SerializeField]
	private Text msg;

	// Token: 0x04001667 RID: 5735
	public int questionId;

	// Token: 0x04001668 RID: 5736
	public BtnCell btnCell;

	// Token: 0x04001669 RID: 5737
	private Avatar player;

	// Token: 0x0400166A RID: 5738
	public GameObject ChildPanel;

	// Token: 0x0400166B RID: 5739
	public GameObject ChildSelect;
}
