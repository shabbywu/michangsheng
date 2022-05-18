using System;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x02000635 RID: 1589
[CommandInfo("YSSea", "关闭当前海域机制", "关闭当前海域机制", 0)]
[AddComponentMenu("")]
public class CmdCloseSeaJiZhi : Command
{
	// Token: 0x0600277A RID: 10106 RVA: 0x00134934 File Offset: 0x00132B34
	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		if (int.TryParse(Tools.getScreenName().Replace("Sea", ""), out num))
		{
			player.EndlessSeaBoss[num.ToString()].SetField("Close", true);
			foreach (MapSeaCompent mapSeaCompent in Object.FindObjectsOfType<MapSeaCompent>())
			{
				mapSeaCompent.WhetherHasJiZhi = mapSeaCompent.HasBoss();
				mapSeaCompent.Refresh();
			}
		}
		this.Continue();
	}
}
