using Fungus;
using UnityEngine;

[CommandInfo("渡劫", "保存神游前场景名称", "保存神游前场景名称", 0)]
[AddComponentMenu("")]
public class CmdSaveSceneNameBeforeShenYou : Command
{
	public override void OnEnter()
	{
		string text = SceneEx.NowSceneName;
		if (text == "S101")
		{
			text = $"DongFu{DongFuManager.NowDongFuID}";
		}
		PlayerEx.Player.TianJieBeforeShenYouSceneName = text;
		Continue();
	}
}
