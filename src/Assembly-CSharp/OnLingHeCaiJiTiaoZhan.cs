using System;
using CaiJi;
using Fungus;
using UnityEngine;

// Token: 0x0200028E RID: 654
[EventHandlerInfo("NPCJiaoHu", "采集灵核遇到挑战", "采集灵核遇到挑战")]
[AddComponentMenu("")]
public class OnLingHeCaiJiTiaoZhan : Fungus.EventHandler
{
	// Token: 0x0600179B RID: 6043 RVA: 0x000A2A38 File Offset: 0x000A0C38
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
