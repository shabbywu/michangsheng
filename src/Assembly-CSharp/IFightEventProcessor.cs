using System;
using KBEngine;

// Token: 0x0200047B RID: 1147
public interface IFightEventProcessor
{
	// Token: 0x060023CE RID: 9166
	void SetAvatar(Avatar player, Avatar monstar);

	// Token: 0x060023CF RID: 9167
	void OnStartFight();

	// Token: 0x060023D0 RID: 9168
	void OnUpdateBuff();

	// Token: 0x060023D1 RID: 9169
	void OnUpdateHP();

	// Token: 0x060023D2 RID: 9170
	void OnUpdateLingQi();

	// Token: 0x060023D3 RID: 9171
	void OnUpdateRound();
}
