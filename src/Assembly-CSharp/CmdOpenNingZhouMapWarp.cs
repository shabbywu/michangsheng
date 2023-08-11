using Fungus;
using UnityEngine;

[CommandInfo("YSTool", "打开宁州传送地图", "打开宁州传送地图", 0)]
[AddComponentMenu("")]
public class CmdOpenNingZhouMapWarp : Command
{
	[SerializeField]
	[Tooltip("当前场景")]
	protected string NowScene;

	public override void OnEnter()
	{
		UIMapPanel.Inst.NingZhou.FungusNowScene = NowScene;
		UIMapPanel.Inst.OpenMap(MapArea.NingZhou, UIMapState.Warp);
		Continue();
	}
}
