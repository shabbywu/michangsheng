using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame.Fight
{
	// Token: 0x02000E0C RID: 3596
	public class UIPlayerLingQiController : MonoBehaviour
	{
		// Token: 0x060056E4 RID: 22244 RVA: 0x000042DD File Offset: 0x000024DD
		private void Update()
		{
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x00242E5C File Offset: 0x0024105C
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

		// Token: 0x060056E6 RID: 22246 RVA: 0x00242EAC File Offset: 0x002410AC
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

		// Token: 0x060056E7 RID: 22247 RVA: 0x0003E206 File Offset: 0x0003C406
		public UIFightLingQiPlayerSlot GetTargetLingQiSlot(LingQiType lingQiType)
		{
			return this.SlotList[(int)lingQiType];
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x00242EE4 File Offset: 0x002410E4
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

		// Token: 0x04005692 RID: 22162
		public List<UIFightLingQiPlayerSlot> SlotList;

		// Token: 0x04005693 RID: 22163
		public Sprite CountBGSmall;

		// Token: 0x04005694 RID: 22164
		public Sprite CountBGBig;

		// Token: 0x04005695 RID: 22165
		public GameObject MoBG;

		// Token: 0x04005696 RID: 22166
		private float refreshCD;
	}
}
