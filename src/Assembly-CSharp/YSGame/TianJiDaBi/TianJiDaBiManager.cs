using System;
using System.Collections.Generic;
using System.Diagnostics;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A92 RID: 2706
	public static class TianJiDaBiManager
	{
		// Token: 0x06004BC9 RID: 19401 RVA: 0x002044B9 File Offset: 0x002026B9
		public static Match GetNowMatch()
		{
			return PlayerEx.Player.StreamData.TianJiDaBiSaveData.NowMatch;
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x002044D0 File Offset: 0x002026D0
		public static void OnAddTime()
		{
			TianJiDaBiSaveData tianJiDaBiSaveData = PlayerEx.Player.StreamData.TianJiDaBiSaveData;
			int year = PlayerEx.Player.worldTimeMag.getNowTime().Year;
			if (tianJiDaBiSaveData.HistotyMatchList == null || tianJiDaBiSaveData.HistotyMatchList.Count == 0)
			{
				Debug.Log("没有大比数据，自动结算一次");
				TianJiDaBiManager.OnTimeSimDaBi();
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
					TianJiDaBiManager.OnTimeSimDaBi();
					int num4 = lastMatchYear + 100;
					tianJiDaBiSaveData.LastMatch.MatchYear = num4;
					tianJiDaBiSaveData.LastMatchYear = num4;
					TianJiDaBiManager.AddMatchPlayerEvent(tianJiDaBiSaveData.LastMatch, false);
					TianJiDaBiManager.SendRewardToNPC(tianJiDaBiSaveData.LastMatch);
				}
			}
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x002045AC File Offset: 0x002027AC
		public static void CmdTianJiDaBiStart(bool playerJoin, List<int> jiaSaiNPCList = null)
		{
			Avatar player = PlayerEx.Player;
			int num = 48;
			List<int> list = new List<int>();
			if (jiaSaiNPCList != null)
			{
				foreach (int npcid in jiaSaiNPCList)
				{
					int num2 = NPCEx.NPCIDToNew(npcid);
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
			List<int> list2 = TianJiDaBiManager.RollDaBiPlayer(num);
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
				daBiPlayer.Level = (int)player.level;
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
			match.PlayerList = match.PlayerList.RandomSort<DaBiPlayer>();
			player.StreamData.TianJiDaBiSaveData.NowMatch = match;
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x00204788 File Offset: 0x00202988
		public static void OnTimeSimDaBi()
		{
			TianJiDaBiManager.IsOnSim = true;
			List<int> npcids = TianJiDaBiManager.RollDaBiPlayer(48);
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
				Debug.Log(string.Format("排名:{0}\tID:{1}\t名字：{2}\t战斗力:{3}\t大分:{4}\t小分:{5}\t隐藏分:{6}", new object[]
				{
					j + 1,
					daBiPlayer.ID,
					daBiPlayer.Name,
					daBiPlayer.Atk,
					daBiPlayer.BigScore,
					daBiPlayer.SmallScore,
					daBiPlayer.HideScore
				}));
			}
			TianJiDaBiManager.AddLastMatchData(match);
			TianJiDaBiManager.IsOnSim = false;
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x002048BC File Offset: 0x00202ABC
		public static void AddLastMatchData(Match match)
		{
			TianJiDaBiSaveData tianJiDaBiSaveData = PlayerEx.Player.StreamData.TianJiDaBiSaveData;
			tianJiDaBiSaveData.LastMatch = match;
			tianJiDaBiSaveData.LastMatchIndex = match.MatchIndex;
			tianJiDaBiSaveData.LastMatchYear = match.MatchYear;
			tianJiDaBiSaveData.HistotyMatchList.Add(match);
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x002048F8 File Offset: 0x00202AF8
		public static void AddMatchPlayerEvent(Match match, bool isNowTime = false)
		{
			int playerCount = match.PlayerCount;
			for (int i = 0; i < playerCount; i++)
			{
				DaBiPlayer daBiPlayer = match.PlayerList[i];
				if (!daBiPlayer.IsWanJia)
				{
					string time;
					if (isNowTime)
					{
						time = PlayerEx.Player.worldTimeMag.nowTime;
					}
					else
					{
						time = string.Format("{0}/12/31 0:00:00", match.MatchYear);
					}
					if (match.MatchYear == 1950)
					{
						Debug.LogError(string.Format("检测到大比1950，请注意检查附近代码，此次不计入NPC重要事件，ID:{0},Name:{1}", daBiPlayer.ID, daBiPlayer.Name));
					}
					else
					{
						NpcJieSuanManager.inst.npcNoteBook.NoteTianJiDaBi(daBiPlayer.ID, i + 1, time);
					}
				}
			}
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x002049A8 File Offset: 0x00202BA8
		public static void SendRewardToNPC(Match match)
		{
			int playerCount = match.PlayerCount;
			int num = 0;
			while (num < playerCount && num < 10)
			{
				DaBiPlayer daBiPlayer = match.PlayerList[num];
				if (!daBiPlayer.IsWanJia)
				{
					try
					{
						foreach (int itemID in TianJiDaBiReward.GetReward(daBiPlayer.LiuPai, num + 1))
						{
							NpcJieSuanManager.inst.AddItemToNpcBackpack(daBiPlayer.ID, itemID, 1, null, false);
						}
					}
					catch (Exception arg)
					{
						Debug.LogError(string.Format("为NPC添加奖励出现错误，NPCID:{0} 流派:{1}，错误信息:{2}", daBiPlayer.ID, daBiPlayer.LiuPai, arg));
					}
				}
				num++;
			}
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x00204A84 File Offset: 0x00202C84
		public static List<int> RollDaBiPlayer(int rollCount)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			List<int> list = new List<int>();
			Dictionary<int, List<TianJiDaBi>> dictionary = new Dictionary<int, List<TianJiDaBi>>();
			for (int i = 1; i <= 3; i++)
			{
				dictionary[i] = new List<TianJiDaBi>();
			}
			foreach (TianJiDaBi tianJiDaBi in TianJiDaBi.DataList)
			{
				dictionary[tianJiDaBi.YouXian].Add(tianJiDaBi);
			}
			JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
			Dictionary<int, List<Vector2Int>> dictionary2 = new Dictionary<int, List<Vector2Int>>();
			for (int j = 1; j <= 15; j++)
			{
				dictionary2.Add(j, new List<Vector2Int>());
			}
			foreach (string text in avatarJsonData.keys)
			{
				int num = int.Parse(text);
				if (num >= 20000)
				{
					int i2 = avatarJsonData[text]["ActionId"].I;
					if (i2 != 1 && i2 <= 200)
					{
						dictionary2[avatarJsonData[text]["Level"].I].Add(new Vector2Int(num, avatarJsonData[text]["LiuPai"].I));
					}
				}
			}
			for (int k = 1; k <= 3; k++)
			{
				dictionary[k] = dictionary[k].RandomSort<TianJiDaBi>();
			}
			int num2 = 0;
			for (int l = 1; l <= 3; l++)
			{
				if (dictionary[l].Count > 0)
				{
					foreach (TianJiDaBi tianJiDaBi2 in dictionary[l])
					{
						if (num2 >= rollCount - 12)
						{
							break;
						}
						int liuPai = tianJiDaBi2.LiuPai;
						int num3 = 0;
						List<int> list2 = TianJiDaBiManager.SearchLiuPaiNPC(dictionary2, liuPai, 12);
						if (list2.Count > 0)
						{
							num3 = list2.GetRandomOne<int>();
						}
						else
						{
							List<int> list3 = TianJiDaBiManager.SearchLiuPaiNPC(dictionary2, liuPai, 11);
							if (list3.Count > 0)
							{
								num3 = list3.GetRandomOne<int>();
							}
							else
							{
								List<int> list4 = TianJiDaBiManager.SearchLiuPaiNPC(dictionary2, liuPai, 10);
								if (list4.Count > 0)
								{
									num3 = list4.GetRandomOne<int>();
								}
							}
						}
						if (num3 != 0)
						{
							Debug.Log(string.Format("找到元婴期NPC:{0} 流派:{1}", num3, liuPai));
							list.Add(num3);
							num2++;
						}
					}
				}
			}
			for (int m = 1; m <= 3; m++)
			{
				dictionary[m] = dictionary[m].RandomSort<TianJiDaBi>();
			}
			for (int n = 1; n <= 3; n++)
			{
				if (dictionary[n].Count > 0)
				{
					foreach (TianJiDaBi tianJiDaBi3 in dictionary[n])
					{
						if (list.Count >= rollCount)
						{
							break;
						}
						int liuPai2 = tianJiDaBi3.LiuPai;
						int num4 = 0;
						List<int> list5 = TianJiDaBiManager.SearchLiuPaiNPC(dictionary2, liuPai2, 9);
						if (list5.Count > 0)
						{
							num4 = list5.GetRandomOne<int>();
						}
						else
						{
							List<int> list6 = TianJiDaBiManager.SearchLiuPaiNPC(dictionary2, liuPai2, 8);
							if (list6.Count > 0)
							{
								num4 = list6.GetRandomOne<int>();
							}
							else
							{
								List<int> list7 = TianJiDaBiManager.SearchLiuPaiNPC(dictionary2, liuPai2, 7);
								if (list7.Count > 0)
								{
									num4 = list7.GetRandomOne<int>();
								}
							}
						}
						if (num4 != 0)
						{
							Debug.Log(string.Format("找到金丹期NPC:{0} 流派:{1}", num4, liuPai2));
							list.Add(num4);
						}
					}
				}
			}
			if (list.Count < rollCount)
			{
				Stopwatch stopwatch2 = new Stopwatch();
				stopwatch2.Start();
				for (int num5 = 1; num5 <= 3; num5++)
				{
					dictionary[num5] = dictionary[num5].RandomSort<TianJiDaBi>();
				}
				foreach (TianJiDaBi tianJiDaBi4 in dictionary[2])
				{
					if (list.Count >= rollCount)
					{
						break;
					}
					int num6 = NPCEx.CreateLiuPaiNPC(tianJiDaBi4.LiuPai, 8);
					Debug.Log(string.Format("生成了金丹期NPC:{0} 流派:{1}", num6, tianJiDaBi4.LiuPai));
					list.Add(num6);
				}
				stopwatch2.Stop();
				Debug.Log(string.Format("生成NPC耗时{0}ms", stopwatch2.ElapsedMilliseconds));
			}
			stopwatch.Stop();
			Debug.Log(string.Format("摇人共耗时{0}ms", stopwatch.ElapsedMilliseconds));
			return list;
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x00204F88 File Offset: 0x00203188
		private static List<int> SearchLiuPaiNPC(Dictionary<int, List<Vector2Int>> dict, int liuPai, int jingJie)
		{
			List<int> list = new List<int>();
			foreach (Vector2Int vector2Int in dict[jingJie])
			{
				if (vector2Int.y == liuPai)
				{
					list.Add(vector2Int.x);
				}
			}
			return list;
		}

		// Token: 0x04004ADF RID: 19167
		public static Random Random = new Random();

		// Token: 0x04004AE0 RID: 19168
		public static bool IsOnSim;
	}
}
