using UnityEngine;

namespace script.Sleep;

public class SleepMag : ISleepMag
{
	public override void Sleep()
	{
		USelectNum.Show("休息{num}天", 1, 30, delegate(int num)
		{
			Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk5000").Inst();
			if (num >= 4)
			{
				PlayerEx.Player.AddHp(PlayerEx.Player.HP_Max);
			}
			else
			{
				PlayerEx.Player.AddHp(PlayerEx.Player.HP_Max * num / 4);
			}
			PlayerEx.Player.worldTimeMag.addTime(num);
		});
	}
}
