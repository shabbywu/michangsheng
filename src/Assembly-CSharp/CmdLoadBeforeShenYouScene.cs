using System;
using Fungus;
using UnityEngine;

[CommandInfo("渡劫", "加载神游前的场景", "加载神游前的场景", 0)]
[AddComponentMenu("")]
public class CmdLoadBeforeShenYouScene : Command
{
	public override void OnEnter()
	{
		string tianJieBeforeShenYouSceneName = PlayerEx.Player.TianJieBeforeShenYouSceneName;
		try
		{
			if (tianJieBeforeShenYouSceneName.StartsWith("DongFu"))
			{
				DongFuManager.LoadDongFuScene(int.Parse(tianJieBeforeShenYouSceneName.Replace("DongFu", "")));
			}
			else
			{
				Tools.instance.loadMapScenes(tianJieBeforeShenYouSceneName);
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"加载神游前场景时出错，场景名:{tianJieBeforeShenYouSceneName}\n{arg}");
		}
		Continue();
	}
}
