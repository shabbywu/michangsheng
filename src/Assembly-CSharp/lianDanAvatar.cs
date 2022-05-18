using System;
using UnityEngine;

// Token: 0x02000619 RID: 1561
public class lianDanAvatar : MonoBehaviour
{
	// Token: 0x060026C7 RID: 9927 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060026C8 RID: 9928 RVA: 0x0012FFC4 File Offset: 0x0012E1C4
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

	// Token: 0x060026C9 RID: 9929 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002114 RID: 8468
	public TooltipScale tooltips;
}
