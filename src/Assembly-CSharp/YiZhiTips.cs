using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020007B3 RID: 1971
public class YiZhiTips : MonoBehaviour
{
	// Token: 0x06003210 RID: 12816 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06003211 RID: 12817 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06003212 RID: 12818 RVA: 0x0018D3FC File Offset: 0x0018B5FC
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
