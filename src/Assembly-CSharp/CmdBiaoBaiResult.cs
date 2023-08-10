using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "获取表白分数", "获取表白分数，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdBiaoBaiResult : Command
{
	[SerializeField]
	[Tooltip("分数类型")]
	protected BiaoBaiScoreType ScoreType;

	public override void OnEnter()
	{
		BiaoBaiManager.CalcBiaoBaiScore();
		Debug.Log((object)BiaoBaiManager.BiaoBaiScore.ToString());
		int value = 0;
		switch (ScoreType)
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
		GetFlowchart().SetIntegerVariable("TmpValue", value);
		Continue();
	}
}
