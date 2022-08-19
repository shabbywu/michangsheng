using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000AB9 RID: 2745
	public class UICacheLingQiController : MonoBehaviour
	{
		// Token: 0x06004CF0 RID: 19696 RVA: 0x0020E95C File Offset: 0x0020CB5C
		public void ChangeCacheSlotNumber(int count)
		{
			if (count < 0 || count > 6)
			{
				Debug.LogError(string.Format("改变缓存槽数量{0}出错，槽数必须为0-6", count));
				return;
			}
			this.nowSlotNum = count;
			foreach (UIFightLingQiCacheSlot uifightLingQiCacheSlot in this.SlotList)
			{
				uifightLingQiCacheSlot.SetNull();
			}
			int num = 0;
			for (int i = 1; i <= count - 1; i++)
			{
				num += i;
			}
			for (int j = 0; j < 6; j++)
			{
				UIFightLingQiCacheSlot uifightLingQiCacheSlot2 = this.SlotList[j];
				if (j < count)
				{
					uifightLingQiCacheSlot2.transform.position = this.LingQiPointList[num + j].position;
					uifightLingQiCacheSlot2.gameObject.SetActive(true);
				}
				else
				{
					uifightLingQiCacheSlot2.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x0020EA40 File Offset: 0x0020CC40
		public void MoveAllLingQiToPlayer()
		{
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				if (this.SlotList[i].LingQiType != LingQiType.Count && this.SlotList[i].LingQiCount > 0)
				{
					UIFightPanel.Inst.PlayerLingQiController.SlotList[(int)this.SlotList[i].LingQiType].LingQiCount += this.SlotList[i].LingQiCount;
					this.SlotList[i].LingQiCount = 0;
				}
			}
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0020EAE0 File Offset: 0x0020CCE0
		public void DestoryAllLingQi()
		{
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				this.SlotList[i].LingQiCount = 0;
			}
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0020EB10 File Offset: 0x0020CD10
		public void SetLingQiLimit(Dictionary<int, int> same, Dictionary<int, int> tong)
		{
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in same)
			{
				this.SlotList[num].LingQiType = (LingQiType)keyValuePair.Key;
				this.SlotList[num].LimitCount = keyValuePair.Value;
				this.SlotList[num].IsLock = true;
				num++;
			}
			foreach (KeyValuePair<int, int> keyValuePair2 in tong)
			{
				this.SlotList[num].LimitCount = keyValuePair2.Value;
				num++;
			}
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0020EBF4 File Offset: 0x0020CDF4
		public Dictionary<LingQiType, int> GetNowCacheLingQi()
		{
			Dictionary<LingQiType, int> dictionary = new Dictionary<LingQiType, int>();
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				if (this.SlotList[i].LingQiCount > 0)
				{
					if (!dictionary.ContainsKey(this.SlotList[i].LingQiType))
					{
						dictionary.Add(this.SlotList[i].LingQiType, 0);
					}
					Dictionary<LingQiType, int> dictionary2 = dictionary;
					LingQiType lingQiType = this.SlotList[i].LingQiType;
					dictionary2[lingQiType] += this.SlotList[i].LingQiCount;
				}
			}
			return dictionary;
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0020EC98 File Offset: 0x0020CE98
		public Dictionary<LingQiType, int> GetNowCacheTongLingQi()
		{
			Dictionary<LingQiType, int> dictionary = new Dictionary<LingQiType, int>();
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				if (this.SlotList[i].LingQiCount > 0 && !this.SlotList[i].IsLock)
				{
					if (!dictionary.ContainsKey(this.SlotList[i].LingQiType))
					{
						dictionary.Add(this.SlotList[i].LingQiType, 0);
					}
					Dictionary<LingQiType, int> dictionary2 = dictionary;
					LingQiType lingQiType = this.SlotList[i].LingQiType;
					dictionary2[lingQiType] += this.SlotList[i].LingQiCount;
				}
			}
			return dictionary;
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0020ED50 File Offset: 0x0020CF50
		public Dictionary<int, int> GetNowCacheLingQiIntDict()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (KeyValuePair<LingQiType, int> keyValuePair in this.GetNowCacheLingQi())
			{
				dictionary.Add((int)keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0020EDB8 File Offset: 0x0020CFB8
		public int GetCacheLingQiSum()
		{
			int num = 0;
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				num += this.SlotList[i].LingQiCount;
			}
			return num;
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x0020EDF0 File Offset: 0x0020CFF0
		public UIFightLingQiCacheSlot GetTargetLingQiSlot(LingQiType lingQiType)
		{
			if (this.NowMoveSame)
			{
				for (int i = 0; i < this.nowSlotNum; i++)
				{
					if (this.SlotList[i].IsLock && this.SlotList[i].LingQiType == lingQiType)
					{
						return this.SlotList[i];
					}
				}
			}
			else
			{
				int j = 0;
				while (j < this.nowSlotNum)
				{
					UIFightLingQiCacheSlot uifightLingQiCacheSlot = this.SlotList[j];
					if (!uifightLingQiCacheSlot.IsLock && uifightLingQiCacheSlot.LingQiType == lingQiType && uifightLingQiCacheSlot.LingQiCount > 0)
					{
						if (uifightLingQiCacheSlot.LingQiCount >= uifightLingQiCacheSlot.LimitCount)
						{
							return null;
						}
						return uifightLingQiCacheSlot;
					}
					else
					{
						j++;
					}
				}
				for (int k = 0; k < this.nowSlotNum; k++)
				{
					UIFightLingQiCacheSlot uifightLingQiCacheSlot2 = this.SlotList[k];
					if (!uifightLingQiCacheSlot2.IsLock && uifightLingQiCacheSlot2.LingQiCount == 0)
					{
						uifightLingQiCacheSlot2.LingQiType = lingQiType;
						return uifightLingQiCacheSlot2;
					}
				}
			}
			return null;
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x0020EED8 File Offset: 0x0020D0D8
		public UIFightLingQiCacheSlot GetTargetTongLingQiSlotWithLimit(int limit)
		{
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				UIFightLingQiCacheSlot uifightLingQiCacheSlot = this.SlotList[i];
				if (!uifightLingQiCacheSlot.IsLock && uifightLingQiCacheSlot.LimitCount == limit && uifightLingQiCacheSlot.LingQiCount < uifightLingQiCacheSlot.LimitCount)
				{
					return uifightLingQiCacheSlot;
				}
			}
			return null;
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x0020EF28 File Offset: 0x0020D128
		public UIFightLingQiCacheSlot GetTongLingQiSlot(LingQiType lingQiType)
		{
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				UIFightLingQiCacheSlot uifightLingQiCacheSlot = this.SlotList[i];
				if (!uifightLingQiCacheSlot.IsLock && uifightLingQiCacheSlot.LingQiType == lingQiType && uifightLingQiCacheSlot.LingQiCount > 0)
				{
					return uifightLingQiCacheSlot;
				}
			}
			return null;
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0020EF70 File Offset: 0x0020D170
		public void RefreshShengKe()
		{
			this.Sheng.SetActive(false);
			this.Ke.SetActive(false);
			if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段)
			{
				Dictionary<int, int> nowCacheLingQiIntDict = this.GetNowCacheLingQiIntDict();
				foreach (KeyValuePair<int, int> keyValuePair in nowCacheLingQiIntDict)
				{
					if (keyValuePair.Key == 5)
					{
						return;
					}
				}
				if (nowCacheLingQiIntDict.Count > 1)
				{
					this.RefreshXiangSheng();
					this.RefreshXiangKe();
				}
			}
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0020F004 File Offset: 0x0020D204
		private void RefreshXiangSheng()
		{
			Dictionary<int, int> nowCacheLingQiIntDict = this.GetNowCacheLingQiIntDict();
			Dictionary<int, int> xiangSheng = Tools.GetXiangSheng();
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in nowCacheLingQiIntDict)
			{
				if (nowCacheLingQiIntDict.ContainsKey(xiangSheng[keyValuePair.Key]))
				{
					num++;
				}
			}
			if (num >= nowCacheLingQiIntDict.Count - 1)
			{
				this.Sheng.SetActive(true);
			}
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0020F08C File Offset: 0x0020D28C
		private void RefreshXiangKe()
		{
			Dictionary<int, int> nowCacheLingQiIntDict = this.GetNowCacheLingQiIntDict();
			Dictionary<int, int> xiangKe = Tools.GetXiangKe();
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in nowCacheLingQiIntDict)
			{
				if (nowCacheLingQiIntDict.ContainsKey(xiangKe[keyValuePair.Key]))
				{
					num++;
				}
			}
			if (num >= nowCacheLingQiIntDict.Count - 1)
			{
				this.Ke.SetActive(true);
			}
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0020F114 File Offset: 0x0020D314
		public void RefreshLingQiCountShow(bool show)
		{
			if (show)
			{
				bool flag = false;
				for (int i = 0; i < this.nowSlotNum; i++)
				{
					if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段)
					{
						this.SlotList[i].LingQiCountText.text = this.SlotList[i].LimitCount.ToString();
						if (!flag && this.SlotList[i].LingQiCount < this.SlotList[i].LimitCount)
						{
							flag = true;
							this.SlotList[i].HighlightObj.SetActive(true);
						}
					}
					else
					{
						this.SlotList[i].LingQiCountText.text = this.SlotList[i].LingQiCount.ToString();
					}
					this.SlotList[i].SetLingQiCountShow(true);
				}
				return;
			}
			for (int j = 0; j < this.nowSlotNum; j++)
			{
				this.SlotList[j].HighlightObj.SetActive(false);
				this.SlotList[j].SetLingQiCountShow(false);
			}
		}

		// Token: 0x04004C01 RID: 19457
		public List<UIFightLingQiCacheSlot> SlotList;

		// Token: 0x04004C02 RID: 19458
		public List<RectTransform> LingQiPointList;

		// Token: 0x04004C03 RID: 19459
		public GameObject Sheng;

		// Token: 0x04004C04 RID: 19460
		public GameObject Ke;

		// Token: 0x04004C05 RID: 19461
		private int nowSlotNum;

		// Token: 0x04004C06 RID: 19462
		public bool NowMoveSame;
	}
}
