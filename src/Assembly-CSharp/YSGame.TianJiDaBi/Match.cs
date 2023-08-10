using System;
using System.Collections.Generic;
using System.Text;
using JSONClass;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[Serializable]
public class Match
{
	public int MatchIndex;

	public int MatchYear;

	public bool PlayerJoin;

	public bool PlayerAbandon;

	public int PlayerCount;

	public int RoundIndex;

	public List<DaBiPlayer> PlayerList;

	private static Dictionary<int, Dictionary<int, NPCLeiXingDate>> NPCLeiXingDict;

	public void Init()
	{
		int year = PlayerEx.Player.worldTimeMag.getNowTime().Year;
		MatchYear = year;
		PlayerList = new List<DaBiPlayer>();
		if (NPCLeiXingDict != null)
		{
			return;
		}
		NPCLeiXingDict = new Dictionary<int, Dictionary<int, NPCLeiXingDate>>();
		for (int i = 1; i <= 15; i++)
		{
			NPCLeiXingDict.Add(i, new Dictionary<int, NPCLeiXingDate>());
		}
		foreach (NPCLeiXingDate data in NPCLeiXingDate.DataList)
		{
			NPCLeiXingDict[data.Level][data.LiuPai] = data;
		}
	}

	public void CreateNPCDaBiPlayers(List<int> npcids)
	{
		foreach (int npcid in npcids)
		{
			UINPCData uINPCData = new UINPCData(npcid);
			uINPCData.RefreshData();
			DaBiPlayer daBiPlayer = new DaBiPlayer();
			daBiPlayer.ID = npcid;
			daBiPlayer.Name = uINPCData.Name;
			daBiPlayer.Title = uINPCData.Title;
			daBiPlayer.DunSu = uINPCData.DunSu;
			daBiPlayer.HP = uINPCData.HP;
			daBiPlayer.LiuPai = uINPCData.LiuPai;
			daBiPlayer.Level = uINPCData.Level;
			NPCLeiXingDate nPCLeiXingDate = NPCLeiXingDict[daBiPlayer.Level][daBiPlayer.LiuPai];
			daBiPlayer.AtkType = nPCLeiXingDate.AttackType;
			daBiPlayer.DefType = nPCLeiXingDate.DefenseType;
			daBiPlayer.MinAtk = nPCLeiXingDate.ShiLi[0];
			daBiPlayer.MaxAtk = nPCLeiXingDate.ShiLi[1];
			PlayerList.Add(daBiPlayer);
		}
	}

	public void NewRound()
	{
		RoundIndex++;
		for (int i = 0; i < PlayerCount; i += 2)
		{
			if (!PlayerList[i].IsWanJia && !PlayerList[i + 1].IsWanJia)
			{
				Fight(RoundIndex, PlayerList[i], PlayerList[i + 1]);
			}
			else if (PlayerAbandon)
			{
				DaBiPlayer win;
				DaBiPlayer fail;
				if (PlayerList[i].IsWanJia)
				{
					win = PlayerList[i + 1];
					fail = PlayerList[i];
				}
				else
				{
					win = PlayerList[i];
					fail = PlayerList[i + 1];
				}
				RecordFight(RoundIndex, win, fail);
			}
		}
	}

	public void AfterRound()
	{
		CalcResult();
		for (int i = 0; i < PlayerCount; i += 2)
		{
			DaBiPlayer daBiPlayer = PlayerList[i];
			DaBiPlayer daBiPlayer2 = PlayerList[i + 1];
			if (daBiPlayer.LastFightID == daBiPlayer2.ID && i + 2 < PlayerCount)
			{
				DaBiPlayer value = daBiPlayer2;
				PlayerList[i + 1] = PlayerList[i + 2];
				PlayerList[i + 2] = value;
			}
		}
	}

	public DaBiPlayer GetPlayer(int id)
	{
		foreach (DaBiPlayer player in PlayerList)
		{
			if (player.ID == id)
			{
				return player;
			}
		}
		return null;
	}

	private void Fight(int RoundIndex, DaBiPlayer a, DaBiPlayer b)
	{
		a.Atk = TianJiDaBiManager.Random.Next() % (a.MaxAtk - a.MinAtk) + a.MinAtk;
		b.Atk = TianJiDaBiManager.Random.Next() % (b.MaxAtk - b.MinAtk) + b.MinAtk;
		TianJiDaBiGongFangKeZhi obj = TianJiDaBiGongFangKeZhi.DataDict[a.DefType];
		TianJiDaBiGongFangKeZhi obj2 = TianJiDaBiGongFangKeZhi.DataDict[b.DefType];
		Type typeFromHandle = typeof(TianJiDaBiGongFangKeZhi);
		int num = (int)typeFromHandle.GetField($"AttackType{b.AtkType}").GetValue(obj2);
		int num2 = (int)typeFromHandle.GetField($"AttackType{a.AtkType}").GetValue(obj);
		int num3 = (((float)a.DunSu >= (float)b.DunSu * 1.1f) ? 10 : 0);
		int num4 = (((float)b.DunSu >= (float)a.DunSu * 1.1f) ? 10 : 0);
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
		RecordFight(RoundIndex, win, fail);
	}

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

	public void CalcResult()
	{
		foreach (DaBiPlayer player in PlayerList)
		{
			player.CalcSmallScore(this);
		}
		foreach (DaBiPlayer player2 in PlayerList)
		{
			player2.CalcHideScore(this);
		}
		PlayerList.Sort(MatchRankSort);
	}

	public void LogPlayerRecord()
	{
		for (int i = 0; i < PlayerCount; i++)
		{
			DaBiPlayer daBiPlayer = PlayerList[i];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"排名:{i + 1} 姓名:{daBiPlayer.Name} id:{daBiPlayer.ID}");
			stringBuilder.AppendLine($"大分:{daBiPlayer.BigScore} 小分:{daBiPlayer.SmallScore} 隐藏分:{daBiPlayer.HideScore}");
			stringBuilder.AppendLine($"MinAtk:{daBiPlayer.MinAtk} MaxAtk:{daBiPlayer.MaxAtk} 等级:{daBiPlayer.Level} 流派:{daBiPlayer.LiuPai}");
			Debug.Log((object)stringBuilder.ToString());
		}
	}
}
