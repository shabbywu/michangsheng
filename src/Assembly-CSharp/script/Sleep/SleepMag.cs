using System;
using UnityEngine;

namespace script.Sleep
{
	// Token: 0x020009ED RID: 2541
	public class SleepMag : ISleepMag
	{
		// Token: 0x06004684 RID: 18052 RVA: 0x001DD14A File Offset: 0x001DB34A
		public override void Sleep()
		{
			USelectNum.Show("休息{num}天", 1, 30, delegate(int num)
			{
				Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk5000").Inst(null);
				if (num >= 4)
				{
					PlayerEx.Player.AddHp(PlayerEx.Player.HP_Max);
				}
				else
				{
					PlayerEx.Player.AddHp(PlayerEx.Player.HP_Max * num / 4);
				}
				PlayerEx.Player.worldTimeMag.addTime(num, 0, 0);
			}, null);
		}
	}
}
