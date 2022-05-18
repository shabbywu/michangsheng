using System;
using UnityEngine;

namespace YSGame.Test
{
	// Token: 0x02000E0D RID: 3597
	public class FightTestCode
	{
		// Token: 0x060056EA RID: 22250 RVA: 0x00243000 File Offset: 0x00241200
		public void RefreshData()
		{
			if (RoundManager.instance == null)
			{
				Debug.Log("需要在战斗中调用");
				return;
			}
			this.PlayerStatus = new AvatarFightStatus(PlayerEx.Player);
			this.DiRenStatus = new AvatarFightStatus(PlayerEx.Player.OtherAvatar);
			this.PlayerStatus.RefreshData();
			this.DiRenStatus.RefreshData();
		}

		// Token: 0x04005697 RID: 22167
		public AvatarFightStatus PlayerStatus;

		// Token: 0x04005698 RID: 22168
		public AvatarFightStatus DiRenStatus;
	}
}
