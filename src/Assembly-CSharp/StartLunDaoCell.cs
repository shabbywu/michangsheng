using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200031D RID: 797
public class StartLunDaoCell : MonoBehaviour
{
	// Token: 0x06001B9B RID: 7067 RVA: 0x000C46B6 File Offset: 0x000C28B6
	public void Click()
	{
		if (this.CanClick)
		{
			LunDaoManager.inst.StartGame();
			return;
		}
		UIPopTip.Inst.Pop("必须选择一个论题", PopTipIconType.叹号);
	}

	// Token: 0x0400161E RID: 5662
	public Image sanJiaoImage;

	// Token: 0x0400161F RID: 5663
	public Image wenZi;

	// Token: 0x04001620 RID: 5664
	public bool CanClick;
}
