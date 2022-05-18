using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class jieyingNextBuff : MonoBehaviour
{
	// Token: 0x0600152A RID: 5418 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000BE5D8 File Offset: 0x000BC7D8
	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			string index = string.Concat(Tools.instance.getPlayer().buffmag.getBuffBySeid(212)[0][2]);
			string key = jsonData.instance.BuffSeidJsonData[5][index]["value1"][0].ToString();
			string desstr = Tools.instance.Code64ToString(jsonData.instance.BuffJsonData[key]["descr"].str);
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = Tools.getDesc(desstr, 1).Replace("[FF00FF]", "[ff8300]");
			Singleton.inventory.showTooltip = true;
			return;
		}
		Singleton.inventory.showTooltip = false;
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}
}
