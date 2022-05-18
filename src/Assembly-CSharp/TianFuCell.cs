using System;
using UnityEngine;

// Token: 0x020005A0 RID: 1440
public class TianFuCell : MonoBehaviour
{
	// Token: 0x06002450 RID: 9296 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x001280A4 File Offset: 0x001262A4
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

	// Token: 0x04001F3D RID: 7997
	public TooltipScale Tooltips;

	// Token: 0x04001F3E RID: 7998
	public int index;

	// Token: 0x04001F3F RID: 7999
	public bool showText;

	// Token: 0x04001F40 RID: 8000
	public string text = "";
}
