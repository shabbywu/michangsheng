using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class JinMaiTips : MonoBehaviour
{
	// Token: 0x06000364 RID: 868 RVA: 0x000130D0 File Offset: 0x000112D0
	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			string str = "经脉";
			string desstr = "【经脉】受【体道】与【气道】感悟影响，【体道】与【气道】感悟越高，【经脉】上限越高\n当【经脉】血量低于50时，每回合抽牌数-2\n当【经脉】归0时，结婴失败，结婴者将修为大退，但下次结婴时，【经脉】上限+5";
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = "[ff8300]" + str + ":[-] " + Tools.getDesc(desstr, 1).Replace("[FF00FF]", "[ff8300]");
			Singleton.inventory.showTooltip = true;
			return;
		}
		Singleton.inventory.showTooltip = false;
	}
}
