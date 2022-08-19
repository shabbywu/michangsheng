using System;
using System.Collections.Generic;
using System.Text;
using JSONClass;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A8F RID: 2703
	[Serializable]
	public class Match
	{
		// Token: 0x06004BB9 RID: 19385 RVA: 0x00203A40 File Offset: 0x00201C40
		public void Init()
		{
			int year = PlayerEx.Player.worldTimeMag.getNowTime().Year;
			this.MatchYear = year;
			this.PlayerList = new List<DaBiPlayer>();
			if (Match.NPCLeiXingDict == null)
			{
				Match.NPCLeiXingDict = new Dictionary<int, Dictionary<int, NPCLeiXingDate>>();
				for (int i = 1; i <= 15; i++)
				{
					Match.NPCLeiXingDict.Add(i, new Dictionary<int, NPCLeiXingDate>());
				}
				foreach (NPCLeiXingDate npcleiXingDate in NPCLeiXingDate.DataList)
				{
					Match.NPCLeiXingDict[npcleiXingDate.Level][npcleiXingDate.LiuPai] = npcleiXingDate;
				}
			}
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x00203B04 File Offset: 0x00201D04
		public void CreateNPCDaBiPlayers(List<int> npcids)
		{
			foreach (int id in npcids)
			{
				UINPCData uinpcdata = new UINPCData(id, false);
				uinpcdata.RefreshData();
				DaBiPlayer daBiPlayer = new DaBiPlayer();
				daBiPlayer.ID = id;
				daBiPlayer.Name = uinpcdata.Name;
				daBiPlayer.Title = uinpcdata.Title;
				daBiPlayer.DunSu = uinpcdata.DunSu;
				daBiPlayer.HP = uinpcdata.HP;
				daBiPlayer.LiuPai = uinpcdata.LiuPai;
				daBiPlayer.Level = uinpcdata.Level;
				NPCLeiXingDate npcleiXingDate = Match.NPCLeiXingDict[daBiPlayer.Level][daBiPlayer.LiuPai];
				daBiPlayer.AtkType = npcleiXingDate.AttackType;
				daBiPlayer.DefType = npcleiXingDate.DefenseType;
				daBiPlayer.MinAtk = npcleiXingDate.ShiLi[0];
				daBiPlayer.MaxAtk = npcleiXingDate.ShiLi[1];
				this.PlayerList.Add(daBiPlayer);
			}
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x00203C20 File Offset: 0x00201E20
		public void NewRound()
		{
			this.RoundIndex++;
			for (int i = 0; i < this.PlayerCount; i += 2)
			{
				if (!this.PlayerList[i].IsWanJia && !this.PlayerList[i + 1].IsWanJia)
				{
					this.Fight(this.RoundIndex, this.PlayerList[i], this.PlayerList[i + 1]);
				}
				else if (this.PlayerAbandon)
				{
					DaBiPlayer win;
					DaBiPlayer fail;
					if (this.PlayerList[i].IsWanJia)
					{
						win = this.PlayerList[i + 1];
						fail = this.PlayerList[i];
					}
					else
					{
						win = this.PlayerList[i];
						fail = this.PlayerList[i + 1];
					}
					this.RecordFight(this.RoundIndex, win, fail);
				}
			}
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x00203D08 File Offset: 0x00201F08
		public void AfterRound()
		{
			this.CalcResult();
			for (int i = 0; i < this.PlayerCount; i += 2)
			{
				DaBiPlayer daBiPlayer = this.PlayerList[i];
				DaBiPlayer daBiPlayer2 = this.PlayerList[i + 1];
				if (daBiPlayer.LastFightID == daBiPlayer2.ID && i + 2 < this.PlayerCount)
				{
					DaBiPlayer value = daBiPlayer2;
					this.PlayerList[i + 1] = this.PlayerList[i + 2];
					this.PlayerList[i + 2] = value;
				}
			}
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x00203D8C File Offset: 0x00201F8C
		public DaBiPlayer GetPlayer(int id)
		{
			foreach (DaBiPlayer daBiPlayer in this.PlayerList)
			{
				if (daBiPlayer.ID == id)
				{
					return daBiPlayer;
				}
			}
			return null;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x00203DE8 File Offset: 0x00201FE8
		private void Fight(int RoundIndex, DaBiPlayer a, DaBiPlayer b)
		{
			a.Atk = TianJiDaBiManager.Random.Next() % (a.MaxAtk - a.MinAtk) + a.MinAtk;
			b.Atk = TianJiDaBiManager.Random.Next() % (b.MaxAtk - b.MinAtk) + b.MinAtk;
			TianJiDaBiGongFangKeZhi obj = TianJiDaBiGongFangKeZhi.DataDict[a.DefType];
			TianJiDaBiGongFangKeZhi obj2 = TianJiDaBiGongFangKeZhi.DataDict[b.DefType];
			Type typeFromHandle = typeof(TianJiDaBiGongFangKeZhi);
			int num = (int)typeFromHandle.GetField(string.Format("AttackType{0}", b.AtkType)).GetValue(obj2);
			int num2 = (int)typeFromHandle.GetField(string.Format("AttackType{0}", a.AtkType)).GetValue(obj);
			int num3 = ((float)a.DunSu >= (float)b.DunSu * 1.1f) ? 10 : 0;
			int num4 = ((float)b.DunSu >= (float)a.DunSu * 1.1f) ? 10 : 0;
			int num5 = 5;
			int num6 = 1;
			int num7 = Mathf.Max(0, a.HP - NPCChuShiShuZiDate.DataDict[a.Level].HP[0]) / num5 * num6;
			int num8 = Mathf.Max(0, b.HP - NPCChuShiShuZiDate.DataDict[b.Level].HP[0]) / num5 * num6;
			float num9 = (float)a.Atk * (1f + (float)(num + num3) / 100f) + (float)num7;
			float num10 = (float)b.Atk * (1f + (float)(num2 + num4) / 100f) + (float)num8;
			DaBiPlayer win;
			DaBiPlayer fail;
			if (num9 == num10)
			{
				if (TianJiDaBiManager.Random.Next() % 100 > 50)
				{
					win = a;
					fail = b;
				}
				else
				{
					win = b;
					fail = a;
				}
			}
			else if (num9 > num10)
			{
				win = a;
				fail = b;
			}
			else
			{
				win = b;
				fail = a;
			}
			this.RecordFight(RoundIndex, win, fail);
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x00203FD8 File Offset: 0x002021D8
		public void RecordFight(int RoundIndex, DaBiPlayer win, DaBiPlayer fail)
		{
			win.BigScore++;
			win.LastFightID = fail.ID;
			fail.LastFightID = win.ID;
			FightRecord item = default(FightRecord);
			item.RoundIndex = RoundIndex;
			item.MeID = win.ID;
			item.TargetID = fail.ID;
			item.WinID = win.ID;
			FightRecord item2 = default(FightRecord);
			item2.RoundIndex = RoundIndex;
			item2.MeID = fail.ID;
			item2.TargetID = win.ID;
			item2.WinID = win.ID;
			win.FightRecords.Add(item);
			fail.FightRecords.Add(item2);
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x00204094 File Offset: 0x00202294
		private int MatchRankSort(DaBiPlayer a, DaBiPlayer b)
		{
			if (a.BigScore != b.BigScore)
			{
				return -a.BigScore.CompareTo(b.BigScore);
			}
			if (a.SmallScore != b.SmallScore)
			{
				return -a.SmallScore.CompareTo(b.SmallScore);
			}
			if (a.HideScore != b.HideScore)
			{
				return -a.HideScore.CompareTo(b.HideScore);
			}
			return -a.Atk.CompareTo(b.Atk);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x00204118 File Offset: 0x00202318
		public void CalcResult()
		{
			foreach (DaBiPlayer daBiPlayer in this.PlayerList)
			{
				daBiPlayer.CalcSmallScore(this);
			}
			foreach (DaBiPlayer daBiPlayer2 in this.PlayerList)
			{
				daBiPlayer2.CalcHideScore(this);
			}
			this.PlayerList.Sort(new Comparison<DaBiPlayer>(this.MatchRankSort));
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x002041C0 File Offset: 0x002023C0
		public void LogPlayerRecord()
		{
			for (int i = 0; i < this.PlayerCount; i++)
			{
				DaBiPlayer daBiPlayer = this.PlayerList[i];
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("排名:{0} 姓名:{1} id:{2}", i + 1, daBiPlayer.Name, daBiPlayer.ID));
				stringBuilder.AppendLine(string.Format("大分:{0} 小分:{1} 隐藏分:{2}", daBiPlayer.BigScore, daBiPlayer.SmallScore, daBiPlayer.HideScore));
				stringBuilder.AppendLine(string.Format("MinAtk:{0} MaxAtk:{1} 等级:{2} 流派:{3}", new object[]
				{
					daBiPlayer.MinAtk,
					daBiPlayer.MaxAtk,
					daBiPlayer.Level,
					daBiPlayer.LiuPai
				}));
				Debug.Log(stringBuilder.ToString());
			}
		}

		// Token: 0x04004AC1 RID: 19137
		public int MatchIndex;

		// Token: 0x04004AC2 RID: 19138
		public int MatchYear;

		// Token: 0x04004AC3 RID: 19139
		public bool PlayerJoin;

		// Token: 0x04004AC4 RID: 19140
		public bool PlayerAbandon;

		// Token: 0x04004AC5 RID: 19141
		public int PlayerCount;

		// Token: 0x04004AC6 RID: 19142
		public int RoundIndex;

		// Token: 0x04004AC7 RID: 19143
		public List<DaBiPlayer> PlayerList;

		// Token: 0x04004AC8 RID: 19144
		private static Dictionary<int, Dictionary<int, NPCLeiXingDate>> NPCLeiXingDict;
	}
}
