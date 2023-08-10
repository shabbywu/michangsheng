using UnityEngine;

public class lianDanAvatar : MonoBehaviour
{
	public TooltipScale tooltips;

	private void Start()
	{
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			int jinDanID = JieDanManager.instence.getJinDanID();
			if (jinDanID != -1)
			{
				tooltips.uILabel.text = "当前金丹属性:" + Tools.Code64(jsonData.instance.JieDanBiao[jinDanID.ToString()]["name"].str) + "\n当前金丹品质:" + JieDanManager.instence.getJinDanPingZhi() + "品";
				tooltips.showTooltip = true;
			}
			else
			{
				tooltips.uILabel.text = "尚未凝结金丹";
				tooltips.showTooltip = true;
			}
		}
		else
		{
			tooltips.showTooltip = false;
		}
	}

	private void Update()
	{
	}
}
