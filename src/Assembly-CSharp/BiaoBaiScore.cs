using System;

// Token: 0x02000337 RID: 823
public class BiaoBaiScore
{
	// Token: 0x06001861 RID: 6241 RVA: 0x000D9CDC File Offset: 0x000D7EDC
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

	// Token: 0x0400137A RID: 4986
	public int FavorScore;

	// Token: 0x0400137B RID: 4987
	public int ZhengXieScore;

	// Token: 0x0400137C RID: 4988
	public int LevelScore;

	// Token: 0x0400137D RID: 4989
	public int AgeScore;

	// Token: 0x0400137E RID: 4990
	public int DaoLvScore;

	// Token: 0x0400137F RID: 4991
	public int DongFuScore;

	// Token: 0x04001380 RID: 4992
	public int DaTiScore;

	// Token: 0x04001381 RID: 4993
	public int OtherTotalScore;

	// Token: 0x04001382 RID: 4994
	public int TotalScore;

	// Token: 0x04001383 RID: 4995
	public bool Player18;

	// Token: 0x04001384 RID: 4996
	public bool NPC18;
}
