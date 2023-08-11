using UnityEngine;

namespace YSGame.Test;

public class FightTestCode
{
	public AvatarFightStatus PlayerStatus;

	public AvatarFightStatus DiRenStatus;

	public void RefreshData()
	{
		if ((Object)(object)RoundManager.instance == (Object)null)
		{
			Debug.Log((object)"需要在战斗中调用");
			return;
		}
		PlayerStatus = new AvatarFightStatus(PlayerEx.Player);
		DiRenStatus = new AvatarFightStatus(PlayerEx.Player.OtherAvatar);
		PlayerStatus.RefreshData();
		DiRenStatus.RefreshData();
	}
}
