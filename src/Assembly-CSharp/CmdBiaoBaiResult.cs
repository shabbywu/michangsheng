using System;
using Fungus;
using UnityEngine;

// Token: 0x0200021D RID: 541
[CommandInfo("YSDongFu", "获取表白分数", "获取表白分数，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdBiaoBaiResult : Command
{
	// Token: 0x060015AB RID: 5547 RVA: 0x00091264 File Offset: 0x0008F464
	public override void OnEnter()
	{
		BiaoBaiManager.CalcBiaoBaiScore();
		Debug.Log(BiaoBaiManager.BiaoBaiScore.ToString());
		int value = 0;
		switch (this.ScoreType)
		{
		case BiaoBaiScoreType.好感分:
			value = BiaoBaiManager.BiaoBaiScore.FavorScore;
			break;
		case BiaoBaiScoreType.正邪分:
			value = BiaoBaiManager.BiaoBaiScore.ZhengXieScore;
			break;
		case BiaoBaiScoreType.境界分:
			value = BiaoBaiManager.BiaoBaiScore.LevelScore;
			break;
		case BiaoBaiScoreType.年龄分:
			value = BiaoBaiManager.BiaoBaiScore.AgeScore;
			break;
		case BiaoBaiScoreType.道侣分:
			value = BiaoBaiManager.BiaoBaiScore.DaoLvScore;
			break;
		case BiaoBaiScoreType.洞府分:
			value = BiaoBaiManager.BiaoBaiScore.DongFuScore;
			break;
		case BiaoBaiScoreType.答题分:
			value = BiaoBaiManager.BiaoBaiScore.DaTiScore;
			break;
		case BiaoBaiScoreType.答题分以外的总分:
			value = BiaoBaiManager.BiaoBaiScore.OtherTotalScore;
			break;
		case BiaoBaiScoreType.总分:
			value = BiaoBaiManager.BiaoBaiScore.TotalScore;
			break;
		}
		this.GetFlowchart().SetIntegerVariable("TmpValue", value);
		this.Continue();
	}

	// Token: 0x04001037 RID: 4151
	[SerializeField]
	[Tooltip("分数类型")]
	protected BiaoBaiScoreType ScoreType;
}
