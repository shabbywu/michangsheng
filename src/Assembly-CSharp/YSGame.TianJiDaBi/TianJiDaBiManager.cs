using System;
using System.Collections.Generic;
using System.Diagnostics;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace YSGame.TianJiDaBi;

public static class TianJiDaBiManager
{
	public static Random Random = new Random();

	public static bool IsOnSim;

	public static Match GetNowMatch()
	{
		return PlayerEx.Player.StreamData.TianJiDaBiSaveData.NowMatch;
	}

	public static void OnAddTime()
	{
		TianJiDaBiSaveData tianJiDaBiSaveData = PlayerEx.Player.StreamData.TianJiDaBiSaveData;
		int year = PlayerEx.Player.worldTimeMag.getNowTime().Year;
		if (tianJiDaBiSaveData.HistotyMatchList == null || tianJiDaBiSaveData.HistotyMatchList.Count == 0)
		{
			Debug.Log((object)"没有大比数据，自动结算一次");
			OnTimeSimDaBi();
			int num = year - (year + 50) % 100;
			tianJiDaBiSaveData.LastMatch.MatchYear = num;
			tianJiDaBiSaveData.LastMatchYear = num;
			return;
		}
		int num2 = year - tianJiDaBiSaveData.LastMatchYear;
		if (num2 > 100)
		{
			int num3 = num2 / 100;
			for (int i = 0; i < num3; i++)
			{
				int lastMatchYear = tianJiDaBiSaveData.LastMatchYear;
				OnTimeSimDaBi();
				int num4 = lastMatchYear + 100;
				tianJiDaBiSaveData.LastMatch.MatchYear = num4;
				tianJiDaBiSaveData.LastMatchYear = num4;
				AddMatchPlayerEvent(tianJiDaBiSaveData.LastMatch);
				SendRewardToNPC(tianJiDaBiSaveData.LastMatch);
			}
		}
	}

	public static void CmdTianJiDaBiStart(bool playerJoin, List<int> jiaSaiNPCList = null)
	{
		Avatar player = PlayerEx.Player;
		int num = 48;
		List<int> list = new List<int>();
		if (jiaSaiNPCList != null)
		{
			foreach (int jiaSaiNPC in jiaSaiNPCList)
			{
				int num2 = NPCEx.NPCIDToNew(jiaSaiNPC);
				if (num2 >= 20000)
				{
					list.Add(num2);
				}
			}
		}
		if (playerJoin)
		{
			num--;
		}
		if (list.Count > 0)
		{
			num -= list.Count;
		}
		List<int> list2 = RollDaBiPlayer(num);
		foreach (int item in list)
		{
			list2.Add(item);
		}
		Match match = new Match();
		match.MatchIndex = player.StreamData.TianJiDaBiSaveData.LastMatchIndex + 1;
		match.Init();
		match.CreateNPCDaBiPlayers(list2);
		if (playerJoin)
		{
			DaBiPlayer daBiPlayer = new DaBiPlayer();
			daBiPlayer.ID = 1;
			daBiPlayer.Name = player.name;
			daBiPlayer.Title = Tools.getMonstarTitle(1);
			daBiPlayer.Level = player.level;
			daBiPlayer.LiuPai = -1;
			daBiPlayer.IsWanJia = true;
			daBiPlayer.Atk = -1;
			daBiPlayer.AtkType = -1;
			daBiPlayer.DefType = -1;
			daBiPlayer.DunSu = player.dunSu;
			daBiPlayer.HP = player.HP_Max;
			daBiPlayer.MaxAtk = -1;
			daBiPlayer.MinAtk = -1;
			match.PlayerList.Add(daBiPlayer);
			match.PlayerJoin = true;
		}
		match.PlayerCount = match.PlayerList.Count;
		match.PlayerList = match.PlayerList.RandomSort();
		player.StreamData.TianJiDaBiSaveData.NowMatch = match;
	}

	public static void OnTimeSimDaBi()
	{
		IsOnSim = true;
		List<int> npcids = RollDaBiPlayer(48);
		Match match = new Match();
		PlayerEx.Player.StreamData.TianJiDaBiSaveData.NowMatch = match;
		match.MatchIndex = PlayerEx.Player.StreamData.TianJiDaBiSaveData.LastMatchIndex + 1;
		match.Init();
		match.CreateNPCDaBiPlayers(npcids);
		match.PlayerCount = match.PlayerList.Count;
		for (int i = 0; i < 6; i++)
		{
			match.NewRound();
			match.AfterRound();
		}
		match.CalcResult();
		for (int j = 0; j < match.PlayerCount; j++)
		{
			DaBiPlayer daBiPlayer = match.PlayerList[j];
			Debug.Log((object)$"排名:{j + 1}\tID:{daBiPlayer.ID}\t名字：{daBiPlayer.Name}\t战斗力:{daBiPlayer.Atk}\t大分:{daBiPlayer.BigScore}\t小分:{daBiPlayer.SmallScore}\t隐藏分:{daBiPlayer.HideScore}");
		}
		AddLastMatchData(match);
		IsOnSim = false;
	}

	public static void AddLastMatchData(Match match)
	{
		TianJiDaBiSaveData tianJiDaBiSaveData = PlayerEx.Player.StreamData.TianJiDaBiSaveData;
		tianJiDaBiSaveData.LastMatch = match;
		tianJiDaBiSaveData.LastMatchIndex = match.MatchIndex;
		tianJiDaBiSaveData.LastMatchYear = match.MatchYear;
		tianJiDaBiSaveData.HistotyMatchList.Add(match);
	}

	public static void AddMatchPlayerEvent(Match match, bool isNowTime = false)
	{
		int playerCount = match.PlayerCount;
		for (int i = 0; i < playerCount; i++)
		{
			DaBiPlayer daBiPlayer = match.PlayerList[i];
			if (!daBiPlayer.IsWanJia)
			{
				string time = ((!isNowTime) ? $"{match.MatchYear}/12/31 0:00:00" : PlayerEx.Player.worldTimeMag.nowTime);
				if (match.MatchYear == 1950)
				{
					Debug.LogError((object)$"检测到大比1950，请注意检查附近代码，此次不计入NPC重要事件，ID:{daBiPlayer.ID},Name:{daBiPlayer.Name}");
				}
				else
				{
					NpcJieSuanManager.inst.npcNoteBook.NoteTianJiDaBi(daBiPlayer.ID, i + 1, time);
				}
			}
		}
	}

	public static void SendRewardToNPC(Match match)
	{
		int playerCount = match.PlayerCount;
		for (int i = 0; i < playerCount && i < 10; i++)
		{
			DaBiPlayer daBiPlayer = match.PlayerList[i];
			if (daBiPlayer.IsWanJia)
			{
				continue;
			}
			try
			{
				foreach (int item in TianJiDaBiReward.GetReward(daBiPlayer.LiuPai, i + 1))
				{
					NpcJieSuanManager.inst.AddItemToNpcBackpack(daBiPlayer.ID, item, 1);
				}
			}
			catch (Exception arg)
			{
				Debug.LogError((object)$"为NPC添加奖励出现错误，NPCID:{daBiPlayer.ID} 流派:{daBiPlayer.LiuPai}，错误信息:{arg}");
			}
		}
	}

	public static List<int> RollDaBiPlayer(int rollCount)
	{
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		List<int> list = new List<int>();
		Dictionary<int, List<JSONClass.TianJiDaBi>> dictionary = new Dictionary<int, List<JSONClass.TianJiDaBi>>();
		for (int i = 1; i <= 3; i++)
		{
			dictionary[i] = new List<JSONClass.TianJiDaBi>();
		}
		foreach (JSONClass.TianJiDaBi data in JSONClass.TianJiDaBi.DataList)
		{
			dictionary[data.YouXian].Add(data);
		}
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		Dictionary<int, List<Vector2Int>> dictionary2 = new Dictionary<int, List<Vector2Int>>();
		for (int j = 1; j <= 15; j++)
		{
			dictionary2.Add(j, new List<Vector2Int>());
		}
		foreach (string key in avatarJsonData.keys)
		{
			int num = int.Parse(key);
			if (num >= 20000)
			{
				int i2 = avatarJsonData[key]["ActionId"].I;
				if (i2 != 1 && i2 <= 200)
				{
					dictionary2[avatarJsonData[key]["Level"].I].Add(new Vector2Int(num, avatarJsonData[key]["LiuPai"].I));
				}
			}
		}
		for (int k = 1; k <= 3; k++)
		{
			dictionary[k] = dictionary[k].RandomSort();
		}
		int num2 = 0;
		for (int l = 1; l <= 3; l++)
		{
			if (dictionary[l].Count <= 0)
			{
				continue;
			}
			foreach (JSONClass.TianJiDaBi item in dictionary[l])
			{
				if (num2 >= rollCount - 12)
				{
					break;
				}
				int liuPai = item.LiuPai;
				int num3 = 0;
				List<int> list2 = SearchLiuPaiNPC(dictionary2, liuPai, 12);
				if (list2.Count > 0)
				{
					num3 = list2.GetRandomOne();
				}
				else
				{
					List<int> list3 = SearchLiuPaiNPC(dictionary2, liuPai, 11);
					if (list3.Count > 0)
					{
						num3 = list3.GetRandomOne();
					}
					else
					{
						List<int> list4 = SearchLiuPaiNPC(dictionary2, liuPai, 10);
						if (list4.Count > 0)
						{
							num3 = list4.GetRandomOne();
						}
					}
				}
				if (num3 != 0)
				{
					Debug.Log((object)$"找到元婴期NPC:{num3} 流派:{liuPai}");
					list.Add(num3);
					num2++;
				}
			}
		}
		for (int m = 1; m <= 3; m++)
		{
			dictionary[m] = dictionary[m].RandomSort();
		}
		for (int n = 1; n <= 3; n++)
		{
			if (dictionary[n].Count <= 0)
			{
				continue;
			}
			foreach (JSONClass.TianJiDaBi item2 in dictionary[n])
			{
				if (list.Count >= rollCount)
				{
					break;
				}
				int liuPai2 = item2.LiuPai;
				int num4 = 0;
				List<int> list5 = SearchLiuPaiNPC(dictionary2, liuPai2, 9);
				if (list5.Count > 0)
				{
					num4 = list5.GetRandomOne();
				}
				else
				{
					List<int> list6 = SearchLiuPaiNPC(dictionary2, liuPai2, 8);
					if (list6.Count > 0)
					{
						num4 = list6.GetRandomOne();
					}
					else
					{
						List<int> list7 = SearchLiuPaiNPC(dictionary2, liuPai2, 7);
						if (list7.Count > 0)
						{
							num4 = list7.GetRandomOne();
						}
					}
				}
				if (num4 != 0)
				{
					Debug.Log((object)$"找到金丹期NPC:{num4} 流派:{liuPai2}");
					list.Add(num4);
				}
			}
		}
		if (list.Count < rollCount)
		{
			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch2.Start();
			for (int num5 = 1; num5 <= 3; num5++)
			{
				dictionary[num5] = dictionary[num5].RandomSort();
			}
			foreach (JSONClass.TianJiDaBi item3 in dictionary[2])
			{
				if (list.Count >= rollCount)
				{
					break;
				}
				int num6 = NPCEx.CreateLiuPaiNPC(item3.LiuPai, 8);
				Debug.Log((object)$"生成了金丹期NPC:{num6} 流派:{item3.LiuPai}");
				list.Add(num6);
			}
			stopwatch2.Stop();
			Debug.Log((object)$"生成NPC耗时{stopwatch2.ElapsedMilliseconds}ms");
		}
		stopwatch.Stop();
		Debug.Log((object)$"摇人共耗时{stopwatch.ElapsedMilliseconds}ms");
		return list;
	}

	private static List<int> SearchLiuPaiNPC(Dictionary<int, List<Vector2Int>> dict, int liuPai, int jingJie)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int>();
		foreach (Vector2Int item in dict[jingJie])
		{
			Vector2Int current = item;
			if (((Vector2Int)(ref current)).y == liuPai)
			{
				list.Add(((Vector2Int)(ref current)).x);
			}
		}
		return list;
	}
}
