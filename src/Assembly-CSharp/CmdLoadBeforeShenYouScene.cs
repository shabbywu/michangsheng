using System;
using Fungus;
using UnityEngine;

// Token: 0x020002A7 RID: 679
[CommandInfo("渡劫", "加载神游前的场景", "加载神游前的场景", 0)]
[AddComponentMenu("")]
public class CmdLoadBeforeShenYouScene : Command
{
	// Token: 0x06001823 RID: 6179 RVA: 0x000A8A50 File Offset: 0x000A6C50
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
				Tools.instance.loadMapScenes(tianJieBeforeShenYouSceneName, true);
			}
		}
		catch (Exception arg)
		{
			Debug.LogError(string.Format("加载神游前场景时出错，场景名:{0}\n{1}", tianJieBeforeShenYouSceneName, arg));
		}
		this.Continue();
	}
}
