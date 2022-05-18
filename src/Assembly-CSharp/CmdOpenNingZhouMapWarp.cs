using System;
using Fungus;
using UnityEngine;

// Token: 0x020004AB RID: 1195
[CommandInfo("YSTool", "打开宁州传送地图", "打开宁州传送地图", 0)]
[AddComponentMenu("")]
public class CmdOpenNingZhouMapWarp : Command
{
	// Token: 0x06001FB5 RID: 8117 RVA: 0x0001A1C8 File Offset: 0x000183C8
	public override void OnEnter()
	{
		UIMapPanel.Inst.NingZhou.FungusNowScene = this.NowScene;
		UIMapPanel.Inst.OpenMap(MapArea.NingZhou, UIMapState.Warp);
		this.Continue();
	}

	// Token: 0x04001B1D RID: 6941
	[SerializeField]
	[Tooltip("当前场景")]
	protected string NowScene;
}
