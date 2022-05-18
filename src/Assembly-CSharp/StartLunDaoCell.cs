using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000480 RID: 1152
public class StartLunDaoCell : MonoBehaviour
{
	// Token: 0x06001ECE RID: 7886 RVA: 0x000198CD File Offset: 0x00017ACD
	public void Click()
	{
		if (this.CanClick)
		{
			LunDaoManager.inst.StartGame();
			return;
		}
		UIPopTip.Inst.Pop("必须选择一个论题", PopTipIconType.叹号);
	}

	// Token: 0x04001A39 RID: 6713
	public Image sanJiaoImage;

	// Token: 0x04001A3A RID: 6714
	public Image wenZi;

	// Token: 0x04001A3B RID: 6715
	public bool CanClick;
}
