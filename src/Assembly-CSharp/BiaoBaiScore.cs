using System;

// Token: 0x0200021B RID: 539
public class BiaoBaiScore
{
	// Token: 0x060015A9 RID: 5545 RVA: 0x000911B8 File Offset: 0x0008F3B8
	public override string ToString()
	{
		return string.Format("表白分数: 好感分:{0} 正邪分:{1} 境界分:{2} 年龄分:{3} 道侣分:{4} 洞府分:{5} 答题分:{6} 玩家年满18:{7} NPC年满18:{8} 总分:{9}", new object[]
		{
			this.FavorScore,
			this.ZhengXieScore,
			this.LevelScore,
			this.AgeScore,
			this.DaoLvScore,
			this.DongFuScore,
			this.DaTiScore,
			this.Player18,
			this.NPC18,
			this.TotalScore
		});
	}

	// Token: 0x04001022 RID: 4130
	public int FavorScore;

	// Token: 0x04001023 RID: 4131
	public int ZhengXieScore;

	// Token: 0x04001024 RID: 4132
	public int LevelScore;

	// Token: 0x04001025 RID: 4133
	public int AgeScore;

	// Token: 0x04001026 RID: 4134
	public int DaoLvScore;

	// Token: 0x04001027 RID: 4135
	public int DongFuScore;

	// Token: 0x04001028 RID: 4136
	public int DaTiScore;

	// Token: 0x04001029 RID: 4137
	public int OtherTotalScore;

	// Token: 0x0400102A RID: 4138
	public int TotalScore;

	// Token: 0x0400102B RID: 4139
	public bool Player18;

	// Token: 0x0400102C RID: 4140
	public bool NPC18;
}
