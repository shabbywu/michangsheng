using CaiJi;
using Fungus;
using UnityEngine;

[EventHandlerInfo("NPCJiaoHu", "采集灵核遇到挑战", "采集灵核遇到挑战")]
[AddComponentMenu("")]
public class OnLingHeCaiJiTiaoZhan : EventHandler
{
	private void Update()
	{
		if (LingHeCaiJiManager.IsOnTiaoZhan)
		{
			LingHeCaiJiManager.IsOnTiaoZhan = false;
			PlayerEx.Player.LingHeCaiJi.SetField("LastTiaoZhan", PlayerEx.Player.worldTimeMag.nowTime);
			Flowchart component = ((Component)this).GetComponent<Flowchart>();
			component.SetIntegerVariable("npcid", LingHeCaiJiManager.LingHeTiaoZhanArg.NPCID);
			component.SetIntegerVariable("LingMaiLevel", LingHeCaiJiManager.LingHeTiaoZhanArg.LingMaiLv);
			ExecuteBlock();
		}
	}
}
