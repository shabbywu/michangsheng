using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class TianFuCell : MonoBehaviour
{
	// Token: 0x0600209E RID: 8350 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x000E5DBC File Offset: 0x000E3FBC
	private void OnHover(bool isOver)
	{
		if (this.index > jsonData.instance.TianFuDescJsonData.Count)
		{
			this.Tooltips.showTooltip = false;
			return;
		}
		try
		{
			if (isOver)
			{
				if (this.showText)
				{
					this.Tooltips.uILabel.text = this.text;
				}
				else
				{
					string text = jsonData.instance.TianFuDescJsonData[this.index.ToString()]["Desc"].str;
					if (text.Contains("{LunDao}"))
					{
						text = text.Replace("{LunDao}", jsonData.instance.LunDaoStateData[Tools.instance.getPlayer().LunDaoState.ToString()]["MiaoShu"].Str + "\n");
					}
					this.Tooltips.uILabel.text = Tools.Code64(text);
				}
				this.Tooltips.showTooltip = true;
			}
			else
			{
				this.Tooltips.showTooltip = false;
			}
		}
		catch (Exception)
		{
			Debug.LogError(this.index);
		}
	}

	// Token: 0x04001A84 RID: 6788
	public TooltipScale Tooltips;

	// Token: 0x04001A85 RID: 6789
	public int index;

	// Token: 0x04001A86 RID: 6790
	public bool showText;

	// Token: 0x04001A87 RID: 6791
	public string text = "";
}
