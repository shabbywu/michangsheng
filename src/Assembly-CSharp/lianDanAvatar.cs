using System;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public class lianDanAvatar : MonoBehaviour
{
	// Token: 0x06002314 RID: 8980 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x000EFA98 File Offset: 0x000EDC98
	private void OnHover(bool isOver)
	{
		if (!isOver)
		{
			this.tooltips.showTooltip = false;
			return;
		}
		int jinDanID = JieDanManager.instence.getJinDanID();
		if (jinDanID != -1)
		{
			this.tooltips.uILabel.text = string.Concat(new object[]
			{
				"当前金丹属性:",
				Tools.Code64(jsonData.instance.JieDanBiao[jinDanID.ToString()]["name"].str),
				"\n当前金丹品质:",
				JieDanManager.instence.getJinDanPingZhi(),
				"品"
			});
			this.tooltips.showTooltip = true;
			return;
		}
		this.tooltips.uILabel.text = "尚未凝结金丹";
		this.tooltips.showTooltip = true;
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C44 RID: 7236
	public TooltipScale tooltips;
}
