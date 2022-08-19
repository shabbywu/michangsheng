using System;
using Fungus;
using UnityEngine;

// Token: 0x0200033A RID: 826
[CommandInfo("YSTool", "打开宁州传送地图", "打开宁州传送地图", 0)]
[AddComponentMenu("")]
public class CmdOpenNingZhouMapWarp : Command
{
	// Token: 0x06001C6A RID: 7274 RVA: 0x000CB9A5 File Offset: 0x000C9BA5
	public override void OnEnter()
	{
		UIMapPanel.Inst.NingZhou.FungusNowScene = this.NowScene;
		UIMapPanel.Inst.OpenMap(MapArea.NingZhou, UIMapState.Warp);
		this.Continue();
	}

	// Token: 0x040016E2 RID: 5858
	[SerializeField]
	[Tooltip("当前场景")]
	protected string NowScene;
}
