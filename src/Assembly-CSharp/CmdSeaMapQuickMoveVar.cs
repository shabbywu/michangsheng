using System;
using Fungus;
using UnityEngine;

// Token: 0x020004AD RID: 1197
[CommandInfo("YSTool", "海上指定快速移动(变量版本)", "海上指定快速移动(不扣钱)", 0)]
[AddComponentMenu("")]
public class CmdSeaMapQuickMoveVar : Command
{
	// Token: 0x06001FB9 RID: 8121 RVA: 0x00110590 File Offset: 0x0010E790
	public override void OnEnter()
	{
		if (this.LingZhouQuality.Value < 0 || this.LingZhouQuality.Value > 6)
		{
			Debug.LogError(string.Format("海上快速移动指令出现异常，灵舟品阶{0}不正确", this.LingZhouQuality.Value));
			this.Continue();
			return;
		}
		if (string.IsNullOrWhiteSpace(this.WarpScene.Value))
		{
			Debug.LogError("海上快速移动指令出现异常，没有配置跳转场景");
			this.Continue();
			return;
		}
		SeaQuickMoveData seaQuickMoveData = UIMapPanel.Inst.Sea.CalcQuickMove(this.StartIndex.Value, this.EndIndex.Value, this.LingZhouQuality.Value, PlayerEx.Player);
		UIMapPanel.Inst.Sea.QuickMoveSlider(seaQuickMoveData.CostDaySum, this.WarpScene.Value);
		this.Continue();
	}

	// Token: 0x04001B22 RID: 6946
	[SerializeField]
	[Tooltip("起始坐标")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable StartIndex;

	// Token: 0x04001B23 RID: 6947
	[SerializeField]
	[Tooltip("结束坐标")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable EndIndex;

	// Token: 0x04001B24 RID: 6948
	[SerializeField]
	[Tooltip("移动结束传送场景")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable WarpScene;

	// Token: 0x04001B25 RID: 6949
	[SerializeField]
	[Tooltip("灵舟品阶(必须0-6，当设为0时，以玩家状态为准进行非灵舟移动)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable LingZhouQuality;
}
