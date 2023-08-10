using System;
using UnityEngine;

public class TianFuCell : MonoBehaviour
{
	public TooltipScale Tooltips;

	public int index;

	public bool showText;

	public string text = "";

	private void Start()
	{
	}

	private void OnHover(bool isOver)
	{
		if (index > jsonData.instance.TianFuDescJsonData.Count)
		{
			Tooltips.showTooltip = false;
			return;
		}
		try
		{
			if (isOver)
			{
				if (showText)
				{
					Tooltips.uILabel.text = this.text;
				}
				else
				{
					string text = jsonData.instance.TianFuDescJsonData[index.ToString()]["Desc"].str;
					if (text.Contains("{LunDao}"))
					{
						text = text.Replace("{LunDao}", jsonData.instance.LunDaoStateData[Tools.instance.getPlayer().LunDaoState.ToString()]["MiaoShu"].Str + "\n");
					}
					Tooltips.uILabel.text = Tools.Code64(text);
				}
				Tooltips.showTooltip = true;
			}
			else
			{
				Tooltips.showTooltip = false;
			}
		}
		catch (Exception)
		{
			Debug.LogError((object)index);
		}
	}
}
