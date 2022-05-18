using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DC2 RID: 3522
	[Serializable]
	public class DaBiPlayer : IComparable
	{
		// Token: 0x060054E0 RID: 21728 RVA: 0x00235728 File Offset: 0x00233928
		public void CalcSmallScore(Match match)
		{
			this.SmallScore = 0;
			foreach (FightRecord fightRecord in this.FightRecords)
			{
				if (fightRecord.WinID == this.ID)
				{
					int targetID = fightRecord.TargetID;
					DaBiPlayer player = match.GetPlayer(targetID);
					this.SmallScore += player.BigScore;
				}
			}
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x002357AC File Offset: 0x002339AC
		public void CalcHideScore(Match match)
		{
			this.HideScore = 0;
			foreach (FightRecord fightRecord in this.FightRecords)
			{
				int targetID = fightRecord.TargetID;
				DaBiPlayer player = match.GetPlayer(targetID);
				this.HideScore += player.SmallScore;
			}
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00235820 File Offset: 0x00233A20
		public void LogRecord(Match match)
		{
			Debug.Log(string.Format("开始输出ID:{0}的对战记录", this.ID));
			for (int i = 0; i < this.FightRecords.Count; i++)
			{
				FightRecord fightRecord = this.FightRecords[i];
				DaBiPlayer player = match.GetPlayer(fightRecord.TargetID);
				Debug.Log(string.Format("第{0}轮，{1}{2}对战{3}{4}，战斗力{5}:{6}，{7}获胜", new object[]
				{
					i + 1,
					this.ID,
					this.Name,
					player.ID,
					player.Name,
					this.Atk,
					player.Atk,
					fightRecord.WinID
				}));
			}
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x002358F8 File Offset: 0x00233AF8
		public int CompareTo(object obj)
		{
			DaBiPlayer daBiPlayer = obj as DaBiPlayer;
			return -this.BigScore.CompareTo(daBiPlayer.BigScore);
		}

		// Token: 0x04005487 RID: 21639
		public int ID;

		// Token: 0x04005488 RID: 21640
		public string Name;

		// Token: 0x04005489 RID: 21641
		public string Title;

		// Token: 0x0400548A RID: 21642
		public int BigScore;

		// Token: 0x0400548B RID: 21643
		public int SmallScore;

		// Token: 0x0400548C RID: 21644
		public int HideScore;

		// Token: 0x0400548D RID: 21645
		public int LastFightID;

		// Token: 0x0400548E RID: 21646
		public List<FightRecord> FightRecords = new List<FightRecord>();

		// Token: 0x0400548F RID: 21647
		public bool IsWanJia;

		// Token: 0x04005490 RID: 21648
		public int Level;

		// Token: 0x04005491 RID: 21649
		public int LiuPai;

		// Token: 0x04005492 RID: 21650
		public int Atk;

		// Token: 0x04005493 RID: 21651
		public int MinAtk;

		// Token: 0x04005494 RID: 21652
		public int MaxAtk;

		// Token: 0x04005495 RID: 21653
		public int DunSu;

		// Token: 0x04005496 RID: 21654
		public int AtkType;

		// Token: 0x04005497 RID: 21655
		public int DefType;
	}
}
