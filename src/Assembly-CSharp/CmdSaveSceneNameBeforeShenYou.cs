using System;
using Fungus;
using UnityEngine;

// Token: 0x020003E4 RID: 996
[CommandInfo("渡劫", "保存神游前场景名称", "保存神游前场景名称", 0)]
[AddComponentMenu("")]
public class CmdSaveSceneNameBeforeShenYou : Command
{
	// Token: 0x06001B20 RID: 6944 RVA: 0x000EFBD8 File Offset: 0x000EDDD8
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
