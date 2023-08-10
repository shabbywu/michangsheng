using System.Collections.Generic;
using UnityEngine;

namespace YSGame.Fight;

public class UICacheLingQiController : MonoBehaviour
{
	public List<UIFightLingQiCacheSlot> SlotList;

	public List<RectTransform> LingQiPointList;

	public GameObject Sheng;

	public GameObject Ke;

	private int nowSlotNum;

	public bool NowMoveSame;

	public void ChangeCacheSlotNumber(int count)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (count < 0 || count > 6)
		{
			Debug.LogError((object)$"改变缓存槽数量{count}出错，槽数必须为0-6");
			return;
		}
		nowSlotNum = count;
		foreach (UIFightLingQiCacheSlot slot in SlotList)
		{
			slot.SetNull();
		}
		int num = 0;
		for (int i = 1; i <= count - 1; i++)
		{
			num += i;
		}
		for (int j = 0; j < 6; j++)
		{
			UIFightLingQiCacheSlot uIFightLingQiCacheSlot = SlotList[j];
			if (j < count)
			{
				((Component)uIFightLingQiCacheSlot).transform.position = ((Transform)LingQiPointList[num + j]).position;
				((Component)uIFightLingQiCacheSlot).gameObject.SetActive(true);
			}
			else
			{
				((Component)uIFightLingQiCacheSlot).gameObject.SetActive(false);
			}
		}
	}

	public void MoveAllLingQiToPlayer()
	{
		for (int i = 0; i < nowSlotNum; i++)
		{
			if (SlotList[i].LingQiType != LingQiType.Count && SlotList[i].LingQiCount > 0)
			{
				UIFightPanel.Inst.PlayerLingQiController.SlotList[(int)SlotList[i].LingQiType].LingQiCount += SlotList[i].LingQiCount;
				SlotList[i].LingQiCount = 0;
			}
		}
	}

	public void DestoryAllLingQi()
	{
		for (int i = 0; i < nowSlotNum; i++)
		{
			SlotList[i].LingQiCount = 0;
		}
	}

	public void SetLingQiLimit(Dictionary<int, int> same, Dictionary<int, int> tong)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> item in same)
		{
			SlotList[num].LingQiType = (LingQiType)item.Key;
			SlotList[num].LimitCount = item.Value;
			SlotList[num].IsLock = true;
			num++;
		}
		foreach (KeyValuePair<int, int> item2 in tong)
		{
			SlotList[num].LimitCount = item2.Value;
			num++;
		}
	}

	public Dictionary<LingQiType, int> GetNowCacheLingQi()
	{
		Dictionary<LingQiType, int> dictionary = new Dictionary<LingQiType, int>();
		for (int i = 0; i < nowSlotNum; i++)
		{
			if (SlotList[i].LingQiCount > 0)
			{
				if (!dictionary.ContainsKey(SlotList[i].LingQiType))
				{
					dictionary.Add(SlotList[i].LingQiType, 0);
				}
				dictionary[SlotList[i].LingQiType] += SlotList[i].LingQiCount;
			}
		}
		return dictionary;
	}

	public Dictionary<LingQiType, int> GetNowCacheTongLingQi()
	{
		Dictionary<LingQiType, int> dictionary = new Dictionary<LingQiType, int>();
		for (int i = 0; i < nowSlotNum; i++)
		{
			if (SlotList[i].LingQiCount > 0 && !SlotList[i].IsLock)
			{
				if (!dictionary.ContainsKey(SlotList[i].LingQiType))
				{
					dictionary.Add(SlotList[i].LingQiType, 0);
				}
				dictionary[SlotList[i].LingQiType] += SlotList[i].LingQiCount;
			}
		}
		return dictionary;
	}

	public Dictionary<int, int> GetNowCacheLingQiIntDict()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (KeyValuePair<LingQiType, int> item in GetNowCacheLingQi())
		{
			dictionary.Add((int)item.Key, item.Value);
		}
		return dictionary;
	}

	public int GetCacheLingQiSum()
	{
		int num = 0;
		for (int i = 0; i < nowSlotNum; i++)
		{
			num += SlotList[i].LingQiCount;
		}
		return num;
	}

	public UIFightLingQiCacheSlot GetTargetLingQiSlot(LingQiType lingQiType)
	{
		if (NowMoveSame)
		{
			for (int i = 0; i < nowSlotNum; i++)
			{
				if (SlotList[i].IsLock && SlotList[i].LingQiType == lingQiType)
				{
					return SlotList[i];
				}
			}
		}
		else
		{
			for (int j = 0; j < nowSlotNum; j++)
			{
				UIFightLingQiCacheSlot uIFightLingQiCacheSlot = SlotList[j];
				if (!uIFightLingQiCacheSlot.IsLock && uIFightLingQiCacheSlot.LingQiType == lingQiType && uIFightLingQiCacheSlot.LingQiCount > 0)
				{
					if (uIFightLingQiCacheSlot.LingQiCount >= uIFightLingQiCacheSlot.LimitCount)
					{
						return null;
					}
					return uIFightLingQiCacheSlot;
				}
			}
			for (int k = 0; k < nowSlotNum; k++)
			{
				UIFightLingQiCacheSlot uIFightLingQiCacheSlot2 = SlotList[k];
				if (!uIFightLingQiCacheSlot2.IsLock && uIFightLingQiCacheSlot2.LingQiCount == 0)
				{
					uIFightLingQiCacheSlot2.LingQiType = lingQiType;
					return uIFightLingQiCacheSlot2;
				}
			}
		}
		return null;
	}

	public UIFightLingQiCacheSlot GetTargetTongLingQiSlotWithLimit(int limit)
	{
		for (int i = 0; i < nowSlotNum; i++)
		{
			UIFightLingQiCacheSlot uIFightLingQiCacheSlot = SlotList[i];
			if (!uIFightLingQiCacheSlot.IsLock && uIFightLingQiCacheSlot.LimitCount == limit && uIFightLingQiCacheSlot.LingQiCount < uIFightLingQiCacheSlot.LimitCount)
			{
				return uIFightLingQiCacheSlot;
			}
		}
		return null;
	}

	public UIFightLingQiCacheSlot GetTongLingQiSlot(LingQiType lingQiType)
	{
		for (int i = 0; i < nowSlotNum; i++)
		{
			UIFightLingQiCacheSlot uIFightLingQiCacheSlot = SlotList[i];
			if (!uIFightLingQiCacheSlot.IsLock && uIFightLingQiCacheSlot.LingQiType == lingQiType && uIFightLingQiCacheSlot.LingQiCount > 0)
			{
				return uIFightLingQiCacheSlot;
			}
		}
		return null;
	}

	public void RefreshShengKe()
	{
		Sheng.SetActive(false);
		Ke.SetActive(false);
		if (UIFightPanel.Inst.UIFightState != UIFightState.释放技能准备灵气阶段)
		{
			return;
		}
		Dictionary<int, int> nowCacheLingQiIntDict = GetNowCacheLingQiIntDict();
		foreach (KeyValuePair<int, int> item in nowCacheLingQiIntDict)
		{
			if (item.Key == 5)
			{
				return;
			}
		}
		if (nowCacheLingQiIntDict.Count > 1)
		{
			RefreshXiangSheng();
			RefreshXiangKe();
		}
	}

	private void RefreshXiangSheng()
	{
		Dictionary<int, int> nowCacheLingQiIntDict = GetNowCacheLingQiIntDict();
		Dictionary<int, int> xiangSheng = Tools.GetXiangSheng();
		int num = 0;
		foreach (KeyValuePair<int, int> item in nowCacheLingQiIntDict)
		{
			if (nowCacheLingQiIntDict.ContainsKey(xiangSheng[item.Key]))
			{
				num++;
			}
		}
		if (num >= nowCacheLingQiIntDict.Count - 1)
		{
			Sheng.SetActive(true);
		}
	}

	private void RefreshXiangKe()
	{
		Dictionary<int, int> nowCacheLingQiIntDict = GetNowCacheLingQiIntDict();
		Dictionary<int, int> xiangKe = Tools.GetXiangKe();
		int num = 0;
		foreach (KeyValuePair<int, int> item in nowCacheLingQiIntDict)
		{
			if (nowCacheLingQiIntDict.ContainsKey(xiangKe[item.Key]))
			{
				num++;
			}
		}
		if (num >= nowCacheLingQiIntDict.Count - 1)
		{
			Ke.SetActive(true);
		}
	}

	public void RefreshLingQiCountShow(bool show)
	{
		if (show)
		{
			bool flag = false;
			for (int i = 0; i < nowSlotNum; i++)
			{
				if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段)
				{
					SlotList[i].LingQiCountText.text = SlotList[i].LimitCount.ToString();
					if (!flag && SlotList[i].LingQiCount < SlotList[i].LimitCount)
					{
						flag = true;
						SlotList[i].HighlightObj.SetActive(true);
					}
				}
				else
				{
					SlotList[i].LingQiCountText.text = SlotList[i].LingQiCount.ToString();
				}
				SlotList[i].SetLingQiCountShow(show: true);
			}
		}
		else
		{
			for (int j = 0; j < nowSlotNum; j++)
			{
				SlotList[j].HighlightObj.SetActive(false);
				SlotList[j].SetLingQiCountShow(show: false);
			}
		}
	}
}
