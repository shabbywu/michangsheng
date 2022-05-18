using System;
using KBEngine;

// Token: 0x02000639 RID: 1593
public interface IFightEventProcessor
{
	// Token: 0x0600278B RID: 10123
	void SetAvatar(Avatar player, Avatar monstar);

	// Token: 0x0600278C RID: 10124
	void OnStartFight();

	// Token: 0x0600278D RID: 10125
	void OnUpdateBuff();

	// Token: 0x0600278E RID: 10126
	void OnUpdateHP();

	// Token: 0x0600278F RID: 10127
	void OnUpdateLingQi();

	// Token: 0x06002790 RID: 10128
	void OnUpdateRound();
}
