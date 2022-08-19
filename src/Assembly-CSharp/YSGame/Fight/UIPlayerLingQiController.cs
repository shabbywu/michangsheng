using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000ACD RID: 2765
	public class UIPlayerLingQiController : MonoBehaviour
	{
		// Token: 0x06004D93 RID: 19859 RVA: 0x00004095 File Offset: 0x00002295
		private void Update()
		{
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x00212D70 File Offset: 0x00210F70
		public void ResetPlayerLingQiCount()
		{
			for (int i = 0; i < 6; i++)
			{
				int num = PlayerEx.Player.cardMag[i];
				if (this.SlotList[i].LingQiCount != num)
				{
					this.SlotList[i].LingQiCount = num;
				}
			}
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x00212DC0 File Offset: 0x00210FC0
		public int GetPlayerLingQiSum()
		{
			this.ResetPlayerLingQiCount();
			int num = 0;
			for (int i = 0; i < 6; i++)
			{
				num += PlayerEx.Player.cardMag[i];
			}
			return num;
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x00212DF5 File Offset: 0x00210FF5
		public UIFightLingQiPlayerSlot GetTargetLingQiSlot(LingQiType lingQiType)
		{
			return this.SlotList[(int)lingQiType];
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x00212E04 File Offset: 0x00211004
		public void RefreshLingQiCount(bool show)
		{
			this.refreshCD = 1f;
			if (show)
			{
				for (int i = 0; i < 6; i++)
				{
					this.SlotList[i].LingQiCountText.text = this.SlotList[i].LingQiCount.ToString();
					this.SlotList[i].SetLingQiCountShow(true);
				}
			}
			else
			{
				for (int j = 0; j < 6; j++)
				{
					this.SlotList[j].SetLingQiCountShow(false);
				}
			}
			Dictionary<LingQiType, int> nowCacheLingQi = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi();
			for (int k = 0; k < 6; k++)
			{
				if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段)
				{
					if (nowCacheLingQi.ContainsKey(this.SlotList[k].LingQiType))
					{
						this.SlotList[k].HighlightObj.SetActive(true);
					}
					else
					{
						this.SlotList[k].HighlightObj.SetActive(false);
					}
				}
				else
				{
					this.SlotList[k].HighlightObj.SetActive(false);
				}
			}
		}

		// Token: 0x04004CB7 RID: 19639
		public List<UIFightLingQiPlayerSlot> SlotList;

		// Token: 0x04004CB8 RID: 19640
		public Sprite CountBGSmall;

		// Token: 0x04004CB9 RID: 19641
		public Sprite CountBGBig;

		// Token: 0x04004CBA RID: 19642
		public GameObject MoBG;

		// Token: 0x04004CBB RID: 19643
		private float refreshCD;
	}
}
