using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class jieyingNextBuff : MonoBehaviour
{
	// Token: 0x0600127B RID: 4731 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x00070C48 File Offset: 0x0006EE48
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

	// Token: 0x0600127D RID: 4733 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}
}
