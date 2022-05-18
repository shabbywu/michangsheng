using System;
using CaiJi;
using Fungus;
using UnityEngine;

// Token: 0x020003BC RID: 956
[EventHandlerInfo("NPCJiaoHu", "采集灵核遇到挑战", "采集灵核遇到挑战")]
[AddComponentMenu("")]
public class OnLingHeCaiJiTiaoZhan : Fungus.EventHandler
{
	// Token: 0x06001A78 RID: 6776 RVA: 0x000E9E30 File Offset: 0x000E8030
	private void Update()
	{
		if (LingHeCaiJiManager.IsOnTiaoZhan)
		{
			LingHeCaiJiManager.IsOnTiaoZhan = false;
			PlayerEx.Player.LingHeCaiJi.SetField("LastTiaoZhan", PlayerEx.Player.worldTimeMag.nowTime);
			Flowchart component = base.GetComponent<Flowchart>();
			component.SetIntegerVariable("npcid", LingHeCaiJiManager.LingHeTiaoZhanArg.NPCID);
			component.SetIntegerVariable("LingMaiLevel", LingHeCaiJiManager.LingHeTiaoZhanArg.LingMaiLv);
			this.ExecuteBlock();
		}
	}
}
