using System;
using UnityEngine;

namespace YSGame.Test
{
	// Token: 0x02000ACE RID: 2766
	public class FightTestCode
	{
		// Token: 0x06004D99 RID: 19865 RVA: 0x00212F20 File Offset: 0x00211120
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

		// Token: 0x04004CBC RID: 19644
		public AvatarFightStatus PlayerStatus;

		// Token: 0x04004CBD RID: 19645
		public AvatarFightStatus DiRenStatus;
	}
}
