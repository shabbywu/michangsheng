using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSTool", "海上指定快速移动(变量版本)", "海上指定快速移动(不扣钱)", 0)]
[AddComponentMenu("")]
public class CmdSeaMapQuickMoveVar : Command
{
	[SerializeField]
	[Tooltip("起始坐标")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable StartIndex;

	[SerializeField]
	[Tooltip("结束坐标")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable EndIndex;

	[SerializeField]
	[Tooltip("移动结束传送场景")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	protected StringVariable WarpScene;

	[SerializeField]
	[Tooltip("灵舟品阶(必须0-6，当设为0时，以玩家状态为准进行非灵舟移动)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable LingZhouQuality;

	public override void OnEnter()
	{
		if (LingZhouQuality.Value < 0 || LingZhouQuality.Value > 6)
		{
			Debug.LogError((object)$"海上快速移动指令出现异常，灵舟品阶{LingZhouQuality.Value}不正确");
			Continue();
		}
		else if (string.IsNullOrWhiteSpace(WarpScene.Value))
		{
			Debug.LogError((object)"海上快速移动指令出现异常，没有配置跳转场景");
			Continue();
		}
		else
		{
			SeaQuickMoveData seaQuickMoveData = UIMapPanel.Inst.Sea.CalcQuickMove(StartIndex.Value, EndIndex.Value, LingZhouQuality.Value, PlayerEx.Player);
			UIMapPanel.Inst.Sea.QuickMoveSlider(seaQuickMoveData.CostDaySum, WarpScene.Value);
			Continue();
		}
	}
}
