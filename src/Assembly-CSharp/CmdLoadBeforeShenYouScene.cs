using System;
using Fungus;
using UnityEngine;

// Token: 0x020003E2 RID: 994
[CommandInfo("渡劫", "加载神游前的场景", "加载神游前的场景", 0)]
[AddComponentMenu("")]
public class CmdLoadBeforeShenYouScene : Command
{
	// Token: 0x06001B1C RID: 6940 RVA: 0x000EFB5C File Offset: 0x000EDD5C
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
