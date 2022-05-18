using System;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200035C RID: 860
[CommandInfo("YSPlayer", "获取玩家在大地图的位置", "获取玩家在大地图的位置(必须在大地图上才能使用)，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerNowMapIndex : Command
{
	// Token: 0x060018E0 RID: 6368 RVA: 0x000DE568 File Offset: 0x000DC768
	public override void OnEnter()
	{
		string name = SceneManager.GetActiveScene().name;
		Flowchart flowchart = this.GetFlowchart();
		if (name == "AllMaps")
		{
			flowchart.SetIntegerVariable("TmpValue", PlayerEx.Player.NowMapIndex);
		}
		else
		{
			Debug.Log("错误调用了指令，获取玩家在大地图的位置指令必须在大地图调用，调用者 " + flowchart.GetParentName() + " " + this.ParentBlock.BlockName);
		}
		this.Continue();
	}
}
