using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E7 RID: 999
public class UIGanYingFight : MonoBehaviour
{
	// Token: 0x06001B38 RID: 6968 RVA: 0x000F0618 File Offset: 0x000EE818
	public void Refresh()
	{
		int num = this.Processor.miShuData.RoundLimit - RoundManager.instance.StaticRoundNum + 1;
		this.RoundText.text = string.Format("剩余{0}回合", num);
		this.DescText.text = (this.Processor.miShuData.desc ?? "");
		this.DangQianJinDuText.text = string.Format("当前进度：{0}", this.Processor.recordValue);
		this.LiShiJinDuText.text = string.Format("历史进度：{0}", this.Processor.GetSaveRecordValue());
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x0001701E File Offset: 0x0001521E
	private void Update()
	{
		if (RoundManager.instance == null || this.Processor == null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040016F9 RID: 5881
	public TianJieMiShuLingWuFightEventProcessor Processor;

	// Token: 0x040016FA RID: 5882
	public Text RoundText;

	// Token: 0x040016FB RID: 5883
	public Text DescText;

	// Token: 0x040016FC RID: 5884
	public Text DangQianJinDuText;

	// Token: 0x040016FD RID: 5885
	public Text LiShiJinDuText;
}
