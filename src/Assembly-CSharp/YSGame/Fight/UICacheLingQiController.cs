using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000DF5 RID: 3573
	public class UICacheLingQiController : MonoBehaviour
	{
		// Token: 0x06005639 RID: 22073 RVA: 0x0023F1C0 File Offset: 0x0023D3C0
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

		// Token: 0x0600563A RID: 22074 RVA: 0x0023F2A4 File Offset: 0x0023D4A4
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

		// Token: 0x0600563B RID: 22075 RVA: 0x0023F344 File Offset: 0x0023D544
		public void DestoryAllLingQi()
		{
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				this.SlotList[i].LingQiCount = 0;
			}
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x0023F374 File Offset: 0x0023D574
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

		// Token: 0x0600563D RID: 22077 RVA: 0x0023F458 File Offset: 0x0023D658
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

		// Token: 0x0600563E RID: 22078 RVA: 0x0023F4FC File Offset: 0x0023D6FC
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

		// Token: 0x0600563F RID: 22079 RVA: 0x0023F5B4 File Offset: 0x0023D7B4
		public Dictionary<int, int> GetNowCacheLingQiIntDict()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (KeyValuePair<LingQiType, int> keyValuePair in this.GetNowCacheLingQi())
			{
				dictionary.Add((int)keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x0023F61C File Offset: 0x0023D81C
		public int GetCacheLingQiSum()
		{
			int num = 0;
			for (int i = 0; i < this.nowSlotNum; i++)
			{
				num += this.SlotList[i].LingQiCount;
			}
			return num;
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x0023F654 File Offset: 0x0023D854
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

		// Token: 0x06005642 RID: 22082 RVA: 0x0023F73C File Offset: 0x0023D93C
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

		// Token: 0x06005643 RID: 22083 RVA: 0x0023F78C File Offset: 0x0023D98C
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

		// Token: 0x06005644 RID: 22084 RVA: 0x0023F7D4 File Offset: 0x0023D9D4
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

		// Token: 0x06005645 RID: 22085 RVA: 0x0023F868 File Offset: 0x0023DA68
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

		// Token: 0x06005646 RID: 22086 RVA: 0x0023F8F0 File Offset: 0x0023DAF0
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

		// Token: 0x06005647 RID: 22087 RVA: 0x0023F978 File Offset: 0x0023DB78
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

		// Token: 0x040055D8 RID: 21976
		public List<UIFightLingQiCacheSlot> SlotList;

		// Token: 0x040055D9 RID: 21977
		public List<RectTransform> LingQiPointList;

		// Token: 0x040055DA RID: 21978
		public GameObject Sheng;

		// Token: 0x040055DB RID: 21979
		public GameObject Ke;

		// Token: 0x040055DC RID: 21980
		private int nowSlotNum;

		// Token: 0x040055DD RID: 21981
		public bool NowMoveSame;
	}
}
