using System;
using Fungus;
using UnityEngine;

// Token: 0x020002A9 RID: 681
[CommandInfo("渡劫", "保存神游前场景名称", "保存神游前场景名称", 0)]
[AddComponentMenu("")]
public class CmdSaveSceneNameBeforeShenYou : Command
{
	// Token: 0x06001827 RID: 6183 RVA: 0x000A8ADC File Offset: 0x000A6CDC
	public override void OnEnter()
	{
		string text = SceneEx.NowSceneName;
		if (text == "S101")
		{
			text = string.Format("DongFu{0}", DongFuManager.NowDongFuID);
		}
		PlayerEx.Player.TianJieBeforeShenYouSceneName = text;
		this.Continue();
	}
}
