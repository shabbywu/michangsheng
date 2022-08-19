using System;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x02000477 RID: 1143
[CommandInfo("YSSea", "关闭当前海域机制", "关闭当前海域机制", 0)]
[AddComponentMenu("")]
public class CmdCloseSeaJiZhi : Command
{
	// Token: 0x060023BD RID: 9149 RVA: 0x000F4A18 File Offset: 0x000F2C18
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
