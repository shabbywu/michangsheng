using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "获取玩家洞府数量", "获取玩家洞府数量，并赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetDongFuCount : Command
{
	public override void OnEnter()
	{
		GetFlowchart().SetIntegerVariable("TmpValue", DongFuManager.GetPlayerDongFuCount());
		Continue();
	}
}
