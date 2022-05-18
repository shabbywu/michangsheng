using System;

namespace YSGame.Fight
{
	// Token: 0x02000E04 RID: 3588
	public enum UIFightState
	{
		// Token: 0x0400564C RID: 22092
		None,
		// Token: 0x0400564D RID: 22093
		自己回合普通状态,
		// Token: 0x0400564E RID: 22094
		敌人回合,
		// Token: 0x0400564F RID: 22095
		释放技能准备灵气阶段,
		// Token: 0x04005650 RID: 22096
		回合结束弃置灵气阶段
	}
}
