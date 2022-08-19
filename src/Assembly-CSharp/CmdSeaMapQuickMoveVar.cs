using System;
using Fungus;
using UnityEngine;

// Token: 0x0200033C RID: 828
[CommandInfo("YSTool", "海上指定快速移动(变量版本)", "海上指定快速移动(不扣钱)", 0)]
[AddComponentMenu("")]
public class CmdSeaMapQuickMoveVar : Command
{
	// Token: 0x06001C6E RID: 7278 RVA: 0x000CBA78 File Offset: 0x000C9C78
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

	// Token: 0x040016E7 RID: 5863
	[SerializeField]
	[Tooltip("起始坐标")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable StartIndex;

	// Token: 0x040016E8 RID: 5864
	[SerializeField]
	[Tooltip("结束坐标")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable EndIndex;

	// Token: 0x040016E9 RID: 5865
	[SerializeField]
	[Tooltip("移动结束传送场景")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable WarpScene;

	// Token: 0x040016EA RID: 5866
	[SerializeField]
	[Tooltip("灵舟品阶(必须0-6，当设为0时，以玩家状态为准进行非灵舟移动)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable LingZhouQuality;
}
