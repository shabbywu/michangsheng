using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[Serializable]
public class DaBiPlayer : IComparable
{
	public int ID;

	public string Name;

	public string Title;

	public int BigScore;

	public int SmallScore;

	public int HideScore;

	public int LastFightID;

	public List<FightRecord> FightRecords = new List<FightRecord>();

	public bool IsWanJia;

	public int Level;

	public int LiuPai;

	public int Atk;

	public int MinAtk;

	public int MaxAtk;

	public int DunSu;

	public int AtkType;

	public int DefType;

	public int HP;

	public void CalcSmallScore(Match match)
	{
		SmallScore = 0;
		foreach (FightRecord fightRecord in FightRecords)
		{
			if (fightRecord.WinID == ID)
			{
				int targetID = fightRecord.TargetID;
				DaBiPlayer player = match.GetPlayer(targetID);
				SmallScore += player.BigScore;
			}
		}
	}

	public void CalcHideScore(Match match)
	{
		HideScore = 0;
		foreach (FightRecord fightRecord in FightRecords)
		{
			int targetID = fightRecord.TargetID;
			DaBiPlayer player = match.GetPlayer(targetID);
			HideScore += player.SmallScore;
		}
	}

	public void LogRecord(Match match)
	{
		Debug.Log((object)$"开始输出ID:{ID}的对战记录");
		for (int i = 0; i < FightRecords.Count; i++)
		{
			FightRecord fightRecord = FightRecords[i];
			DaBiPlayer player = match.GetPlayer(fightRecord.TargetID);
			Debug.Log((object)$"第{i + 1}轮，{ID}{Name}对战{player.ID}{player.Name}，战斗力{Atk}:{player.Atk}，{fightRecord.WinID}获胜");
		}
	}

	public int CompareTo(object obj)
	{
		DaBiPlayer daBiPlayer = obj as DaBiPlayer;
		return -BigScore.CompareTo(daBiPlayer.BigScore);
	}
}
