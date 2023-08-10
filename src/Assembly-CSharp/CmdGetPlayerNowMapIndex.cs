using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

[CommandInfo("YSPlayer", "获取玩家在大地图的位置", "获取玩家在大地图的位置(必须在大地图上才能使用)，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerNowMapIndex : Command
{
	public override void OnEnter()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		Scene activeScene = SceneManager.GetActiveScene();
		string name = ((Scene)(ref activeScene)).name;
		Flowchart flowchart = GetFlowchart();
		if (name == "AllMaps")
		{
			flowchart.SetIntegerVariable("TmpValue", PlayerEx.Player.NowMapIndex);
		}
		else
		{
			Debug.Log((object)("错误调用了指令，获取玩家在大地图的位置指令必须在大地图调用，调用者 " + flowchart.GetParentName() + " " + ParentBlock.BlockName));
		}
		Continue();
	}
}
