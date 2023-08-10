public class BiaoBaiScore
{
	public int FavorScore;

	public int ZhengXieScore;

	public int LevelScore;

	public int AgeScore;

	public int DaoLvScore;

	public int DongFuScore;

	public int DaTiScore;

	public int OtherTotalScore;

	public int TotalScore;

	public bool Player18;

	public bool NPC18;

	public override string ToString()
	{
		return $"表白分数: 好感分:{FavorScore} 正邪分:{ZhengXieScore} 境界分:{LevelScore} 年龄分:{AgeScore} 道侣分:{DaoLvScore} 洞府分:{DongFuScore} 答题分:{DaTiScore} 玩家年满18:{Player18} NPC年满18:{NPC18} 总分:{TotalScore}";
	}
}
