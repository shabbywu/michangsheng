using System;
using GUIPackage;
using UnityEngine;

// Token: 0x0200051F RID: 1311
public class YiZhiTips : MonoBehaviour
{
	// Token: 0x060029FD RID: 10749 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x00140234 File Offset: 0x0013E434
	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			string str = "意志";
			string desstr = "【意志】受【心境】与【神道】感悟影响，【心境】与【神道】感悟越高，【意志】上限越高\n当【意志】低于50时，玩家受到的心魔伤害将提升20%\n当【意志】归0时，结婴失败，结婴者将身死道消";
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = "[ff8300]" + str + ":[-] " + Tools.getDesc(desstr, 1).Replace("[FF00FF]", "[ff8300]");
			Singleton.inventory.showTooltip = true;
			return;
		}
		Singleton.inventory.showTooltip = false;
	}
}
