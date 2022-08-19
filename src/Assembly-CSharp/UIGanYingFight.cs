using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AF RID: 687
public class UIGanYingFight : MonoBehaviour
{
	// Token: 0x06001846 RID: 6214 RVA: 0x000A97EC File Offset: 0x000A79EC
	public void Refresh()
	{
		int num = this.Processor.miShuData.RoundLimit - RoundManager.instance.StaticRoundNum + 1;
		this.RoundText.text = string.Format("剩余{0}回合", num);
		this.DescText.text = (this.Processor.miShuData.desc ?? "");
		this.DangQianJinDuText.text = string.Format("当前进度：{0}", this.Processor.recordValue);
		this.LiShiJinDuText.text = string.Format("历史进度：{0}", this.Processor.GetSaveRecordValue());
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000A98A0 File Offset: 0x000A7AA0
	private void Update()
	{
		if (RoundManager.instance == null || this.Processor == null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400135C RID: 4956
	public TianJieMiShuLingWuFightEventProcessor Processor;

	// Token: 0x0400135D RID: 4957
	public Text RoundText;

	// Token: 0x0400135E RID: 4958
	public Text DescText;

	// Token: 0x0400135F RID: 4959
	public Text DangQianJinDuText;

	// Token: 0x04001360 RID: 4960
	public Text LiShiJinDuText;
}
