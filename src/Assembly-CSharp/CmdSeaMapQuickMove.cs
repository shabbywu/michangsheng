using System;
using Fungus;
using UnityEngine;

// Token: 0x020004AC RID: 1196
[CommandInfo("YSTool", "海上指定快速移动", "海上指定快速移动(不扣钱)", 0)]
[AddComponentMenu("")]
public class CmdSeaMapQuickMove : Command
{
	// Token: 0x06001FB7 RID: 8119 RVA: 0x001104E8 File Offset: 0x0010E6E8
	public override void OnEnter()
	{
		if (this.LingZhouQuality < 0 || this.LingZhouQuality > 6)
		{
			Debug.LogError(string.Format("海上快速移动指令出现异常，灵舟品阶{0}不正确", this.LingZhouQuality));
			this.Continue();
			return;
		}
		if (string.IsNullOrWhiteSpace(this.WarpScene))
		{
			Debug.LogError("海上快速移动指令出现异常，没有配置跳转场景");
			this.Continue();
			return;
		}
		SeaQuickMoveData seaQuickMoveData = UIMapPanel.Inst.Sea.CalcQuickMove(this.StartIndex, this.EndIndex, this.LingZhouQuality, PlayerEx.Player);
		UIMapPanel.Inst.Sea.QuickMoveSlider(seaQuickMoveData.CostDaySum, this.WarpScene);
		this.Continue();
	}

	// Token: 0x04001B1E RID: 6942
	[SerializeField]
	[Tooltip("起始坐标")]
	protected int StartIndex;

	// Token: 0x04001B1F RID: 6943
	[SerializeField]
	[Tooltip("结束坐标")]
	protected int EndIndex;

	// Token: 0x04001B20 RID: 6944
	[SerializeField]
	[Tooltip("移动结束传送场景")]
	protected string WarpScene;

	// Token: 0x04001B21 RID: 6945
	[SerializeField]
	[Tooltip("灵舟品阶(必须0-6，当设为0时，以玩家状态为准进行非灵舟移动)")]
	protected int LingZhouQuality;
}
