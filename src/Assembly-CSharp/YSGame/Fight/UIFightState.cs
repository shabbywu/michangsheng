using System;

namespace YSGame.Fight
{
	// Token: 0x02000AC6 RID: 2758
	public enum UIFightState
	{
		// Token: 0x04004C72 RID: 19570
		None,
		// Token: 0x04004C73 RID: 19571
		自己回合普通状态,
		// Token: 0x04004C74 RID: 19572
		敌人回合,
		// Token: 0x04004C75 RID: 19573
		释放技能准备灵气阶段,
		// Token: 0x04004C76 RID: 19574
		回合结束弃置灵气阶段
	}
}
