using System;

// Token: 0x020002B7 RID: 695
public enum SkillCanUseType
{
	// Token: 0x0400137B RID: 4987
	尚未冷却不能使用 = 1,
	// Token: 0x0400137C RID: 4988
	灵气不足无法使用,
	// Token: 0x0400137D RID: 4989
	Buff层数不足无法使用,
	// Token: 0x0400137E RID: 4990
	Buff种类不足无法使用,
	// Token: 0x0400137F RID: 4991
	超过最多使用次数不能使用,
	// Token: 0x04001380 RID: 4992
	角色死亡不能使用,
	// Token: 0x04001381 RID: 4993
	非自己回合不能使用,
	// Token: 0x04001382 RID: 4994
	魔气无法当做同系灵气不能使用,
	// Token: 0x04001383 RID: 4995
	遁速不足无法使用,
	// Token: 0x04001384 RID: 4996
	血量太高无法使用,
	// Token: 0x04001385 RID: 4997
	神识不足无法使用,
	// Token: 0x04001386 RID: 4998
	寿元不足无法使用,
	// Token: 0x04001387 RID: 4999
	本回合使用过其他技能无法使用,
	// Token: 0x04001388 RID: 5000
	可以使用 = 100
}
