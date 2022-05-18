using System;

// Token: 0x020003F3 RID: 1011
public enum SkillCanUseType
{
	// Token: 0x04001721 RID: 5921
	尚未冷却不能使用 = 1,
	// Token: 0x04001722 RID: 5922
	灵气不足无法使用,
	// Token: 0x04001723 RID: 5923
	Buff层数不足无法使用,
	// Token: 0x04001724 RID: 5924
	Buff种类不足无法使用,
	// Token: 0x04001725 RID: 5925
	超过最多使用次数不能使用,
	// Token: 0x04001726 RID: 5926
	角色死亡不能使用,
	// Token: 0x04001727 RID: 5927
	非自己回合不能使用,
	// Token: 0x04001728 RID: 5928
	魔气无法当做同系灵气不能使用,
	// Token: 0x04001729 RID: 5929
	遁速不足无法使用,
	// Token: 0x0400172A RID: 5930
	血量太高无法使用,
	// Token: 0x0400172B RID: 5931
	神识不足无法使用,
	// Token: 0x0400172C RID: 5932
	寿元不足无法使用,
	// Token: 0x0400172D RID: 5933
	本回合使用过其他技能无法使用,
	// Token: 0x0400172E RID: 5934
	可以使用 = 100
}
