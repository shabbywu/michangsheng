using System;
using System.Collections.Generic;
using System.Text;
using JSONClass;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DC1 RID: 3521
	[Serializable]
	public class Match
	{
		// Token: 0x060054D5 RID: 21717 RVA: 0x00234F34 File Offset: 0x00233134
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

		// Token: 0x060054D6 RID: 21718 RVA: 0x00234FF8 File Offset: 0x002331F8
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

		// Token: 0x060054D7 RID: 21719 RVA: 0x00235108 File Offset: 0x00233308
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

		// Token: 0x060054D8 RID: 21720 RVA: 0x002351F0 File Offset: 0x002333F0
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

		// Token: 0x060054D9 RID: 21721 RVA: 0x00235274 File Offset: 0x00233474
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

		// Token: 0x060054DA RID: 21722 RVA: 0x002352D0 File Offset: 0x002334D0
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
			float num5 = (float)a.Atk * (1f + (float)(num + num3) / 100f);
			float num6 = (float)b.Atk * (1f + (float)(num2 + num4) / 100f);
			DaBiPlayer win;
			DaBiPlayer fail;
			if (num5 == num6)
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
			else if (num5 > num6)
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

		// Token: 0x060054DB RID: 21723 RVA: 0x00235450 File Offset: 0x00233650
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

		// Token: 0x060054DC RID: 21724 RVA: 0x0023550C File Offset: 0x0023370C
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

		// Token: 0x060054DD RID: 21725 RVA: 0x00235590 File Offset: 0x00233790
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

		// Token: 0x060054DE RID: 21726 RVA: 0x00235638 File Offset: 0x00233838
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

		// Token: 0x0400547F RID: 21631
		public int MatchIndex;

		// Token: 0x04005480 RID: 21632
		public int MatchYear;

		// Token: 0x04005481 RID: 21633
		public bool PlayerJoin;

		// Token: 0x04005482 RID: 21634
		public bool PlayerAbandon;

		// Token: 0x04005483 RID: 21635
		public int PlayerCount;

		// Token: 0x04005484 RID: 21636
		public int RoundIndex;

		// Token: 0x04005485 RID: 21637
		public List<DaBiPlayer> PlayerList;

		// Token: 0x04005486 RID: 21638
		private static Dictionary<int, Dictionary<int, NPCLeiXingDate>> NPCLeiXingDict;
	}
}
