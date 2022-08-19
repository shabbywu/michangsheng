using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A90 RID: 2704
	[Serializable]
	public class DaBiPlayer : IComparable
	{
		// Token: 0x06004BC4 RID: 19396 RVA: 0x002042B0 File Offset: 0x002024B0
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

		// Token: 0x06004BC5 RID: 19397 RVA: 0x00204334 File Offset: 0x00202534
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

		// Token: 0x06004BC6 RID: 19398 RVA: 0x002043A8 File Offset: 0x002025A8
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

		// Token: 0x06004BC7 RID: 19399 RVA: 0x00204480 File Offset: 0x00202680
		public int CompareTo(object obj)
		{
			DaBiPlayer daBiPlayer = obj as DaBiPlayer;
			return -this.BigScore.CompareTo(daBiPlayer.BigScore);
		}

		// Token: 0x04004AC9 RID: 19145
		public int ID;

		// Token: 0x04004ACA RID: 19146
		public string Name;

		// Token: 0x04004ACB RID: 19147
		public string Title;

		// Token: 0x04004ACC RID: 19148
		public int BigScore;

		// Token: 0x04004ACD RID: 19149
		public int SmallScore;

		// Token: 0x04004ACE RID: 19150
		public int HideScore;

		// Token: 0x04004ACF RID: 19151
		public int LastFightID;

		// Token: 0x04004AD0 RID: 19152
		public List<FightRecord> FightRecords = new List<FightRecord>();

		// Token: 0x04004AD1 RID: 19153
		public bool IsWanJia;

		// Token: 0x04004AD2 RID: 19154
		public int Level;

		// Token: 0x04004AD3 RID: 19155
		public int LiuPai;

		// Token: 0x04004AD4 RID: 19156
		public int Atk;

		// Token: 0x04004AD5 RID: 19157
		public int MinAtk;

		// Token: 0x04004AD6 RID: 19158
		public int MaxAtk;

		// Token: 0x04004AD7 RID: 19159
		public int DunSu;

		// Token: 0x04004AD8 RID: 19160
		public int AtkType;

		// Token: 0x04004AD9 RID: 19161
		public int DefType;

		// Token: 0x04004ADA RID: 19162
		public int HP;
	}
}
