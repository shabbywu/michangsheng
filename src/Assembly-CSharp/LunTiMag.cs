using System.Collections.Generic;
using UnityEngine;

public class LunTiMag
{
	public Dictionary<int, List<int>> targetLunTiDictionary;

	public List<LunDaoQiu> curLunDianList;

	public LunTiMag()
	{
		curLunDianList = new List<LunDaoQiu>();
	}

	public void CreateLunTi(List<int> lunTiList, int npcId)
	{
		targetLunTiDictionary = new Dictionary<int, List<int>>();
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"];
		int num = 0;
		int num2 = 0;
		foreach (int lunTi in lunTiList)
		{
			num = jSONObject[lunTi.ToString()]["level"].I + 1;
			num2 = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(lunTi) + 1;
			if (num == num2)
			{
				targetLunTiDictionary.Add(lunTi, new List<int> { num + 1 });
			}
			else if (num > num2)
			{
				targetLunTiDictionary.Add(lunTi, new List<int> { num, num2 });
			}
			else
			{
				targetLunTiDictionary.Add(lunTi, new List<int> { num2, num });
			}
		}
	}

	public bool CheckCanHeCheng(ref int minIndex, ref int bigIndex)
	{
		if (curLunDianList.Count < 1)
		{
			return false;
		}
		for (int i = 0; i < curLunDianList.Count; i++)
		{
			if (curLunDianList[i].isNull)
			{
				continue;
			}
			for (int num = curLunDianList.Count - 1; num > i; num--)
			{
				if (curLunDianList[i].wudaoId == curLunDianList[num].wudaoId && curLunDianList[i].level == curLunDianList[num].level)
				{
					minIndex = i;
					bigIndex = num;
					return true;
				}
			}
		}
		minIndex = -1;
		bigIndex = -1;
		return false;
	}

	private bool CheckIsTargetLunTi(ref int wuDaoId)
	{
		foreach (int key in targetLunTiDictionary.Keys)
		{
			int num = 0;
			foreach (int item in targetLunTiDictionary[key])
			{
				foreach (LunDaoQiu curLunDian in curLunDianList)
				{
					if (!curLunDian.isNull && curLunDian.wudaoId == key && curLunDian.level == item)
					{
						num++;
						break;
					}
				}
			}
			if (num == targetLunTiDictionary[key].Count)
			{
				wuDaoId = key;
				return true;
			}
		}
		wuDaoId = -1;
		return false;
	}

	public bool CheckIsTargetLunTi()
	{
		int wuDaoId = -1;
		return CheckIsTargetLunTi(ref wuDaoId);
	}

	public void CompleteLunTi()
	{
		int wuDaoId = 0;
		if (CheckIsTargetLunTi(ref wuDaoId))
		{
			LunDaoManager.inst.hasCompleteLunTi.Add(wuDaoId);
			LunDaoManager.inst.AddWuDaoExp(wuDaoId);
			int num = 0;
			foreach (int item in targetLunTiDictionary[wuDaoId])
			{
				num += jsonData.instance.WuDaoZhiJiaCheng[item.ToString()]["JiaCheng"].I;
			}
			LunDaoManager.inst.AddWuDaoZhi(wuDaoId, num);
			targetLunTiDictionary.Remove(wuDaoId);
			LunDaoManager.inst.lunDaoAmrMag.AddCompleteLunTi(LunDaoManager.inst.lunDaoPanel.lunTiCtrDictionary[wuDaoId].finshIBg, LunDaoManager.inst.lunDaoPanel.lunTiCtrDictionary[wuDaoId].finshImage);
		}
		if (targetLunTiDictionary.Keys.Count < 1)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.论道结束;
			LunDaoManager.inst.GameOver();
		}
	}

	public void LunDianHeCheng()
	{
		int minIndex = -1;
		int bigIndex = -1;
		while (CheckCanHeCheng(ref minIndex, ref bigIndex))
		{
			curLunDianList[minIndex].LevelUp();
			int i = jsonData.instance.LunDaoShouYiData[curLunDianList[minIndex].level.ToString()]["WuDaoZhi"].I;
			LunDaoManager.inst.AddWuDaoZhi(curLunDianList[minIndex].wudaoId, i);
			curLunDianList[bigIndex].SetNull();
			LunDaoManager.inst.lunDaoAmrMag.AddHeCheng(((Component)curLunDianList[minIndex]).transform);
			CompleteLunTi();
		}
	}

	public int GetNullSlot()
	{
		int result = -1;
		for (int i = 0; i < curLunDianList.Count; i++)
		{
			if (curLunDianList[i].isNull)
			{
				return i;
			}
		}
		return result;
	}

	public List<LunDaoCard> GetShengYuLunDian()
	{
		List<LunDaoCard> list = new List<LunDaoCard>();
		foreach (int key in targetLunTiDictionary.Keys)
		{
			foreach (int item in targetLunTiDictionary[key])
			{
				list.Add(new LunDaoCard(key, item));
			}
		}
		List<LunDaoCard> list2 = new List<LunDaoCard>();
		foreach (LunDaoQiu curLunDian in curLunDianList)
		{
			foreach (LunDaoCard item2 in list)
			{
				if (curLunDian.wudaoId == item2.wudaoId && curLunDian.level == item2.level)
				{
					list2.Add(item2);
				}
			}
		}
		foreach (LunDaoCard item3 in list2)
		{
			list.Remove(item3);
		}
		return list;
	}
}
