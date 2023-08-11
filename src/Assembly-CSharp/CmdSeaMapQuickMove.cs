using Fungus;
using UnityEngine;

[CommandInfo("YSTool", "海上指定快速移动", "海上指定快速移动(不扣钱)", 0)]
[AddComponentMenu("")]
public class CmdSeaMapQuickMove : Command
{
	[SerializeField]
	[Tooltip("起始坐标")]
	protected int StartIndex;

	[SerializeField]
	[Tooltip("结束坐标")]
	protected int EndIndex;

	[SerializeField]
	[Tooltip("移动结束传送场景")]
	protected string WarpScene;

	[SerializeField]
	[Tooltip("灵舟品阶(必须0-6，当设为0时，以玩家状态为准进行非灵舟移动)")]
	protected int LingZhouQuality;

	public override void OnEnter()
	{
		if (LingZhouQuality < 0 || LingZhouQuality > 6)
		{
			Debug.LogError((object)$"海上快速移动指令出现异常，灵舟品阶{LingZhouQuality}不正确");
			Continue();
		}
		else if (string.IsNullOrWhiteSpace(WarpScene))
		{
			Debug.LogError((object)"海上快速移动指令出现异常，没有配置跳转场景");
			Continue();
		}
		else
		{
			SeaQuickMoveData seaQuickMoveData = UIMapPanel.Inst.Sea.CalcQuickMove(StartIndex, EndIndex, LingZhouQuality, PlayerEx.Player);
			UIMapPanel.Inst.Sea.QuickMoveSlider(seaQuickMoveData.CostDaySum, WarpScene);
			Continue();
		}
	}
}
