using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai
{
	// Token: 0x02000A60 RID: 2656
	public class AvatarCtr : MonoBehaviour
	{
		// Token: 0x06004472 RID: 17522 RVA: 0x001D463C File Offset: 0x001D283C
		public void AllAvatarThinkItem()
		{
			foreach (PaiMaiAvatar paiMaiAvatar in this.AvatarList)
			{
				paiMaiAvatar.ThinKCurShop();
			}
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x00030F88 File Offset: 0x0002F188
		public void AvatarStart()
		{
			this.index = 0;
			base.Invoke("AvatarAddPrice", 0.75f);
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x001D468C File Offset: 0x001D288C
		private void AvatarAddPrice()
		{
			if (this.index >= this.AvatarList.Count)
			{
				base.Invoke("EndRound", 0.5f);
				return;
			}
			if (this.AvatarList[this.index].State == PaiMaiAvatar.StateType.放弃)
			{
				SingletonMono<PaiMaiUiMag>.Instance.CurAvatar = this.AvatarList[this.index];
				this.index++;
				this.AvatarAddPrice();
				return;
			}
			SingletonMono<PaiMaiUiMag>.Instance.CurAvatar = this.AvatarList[this.index];
			SingletonMono<PaiMaiUiMag>.Instance.AddPrice(0);
			this.index++;
			if (this.index >= this.AvatarList.Count)
			{
				base.Invoke("EndRound", 0.5f);
				return;
			}
			base.Invoke("AvatarAddPrice", 0.75f);
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x001D4770 File Offset: 0x001D2970
		public void AvatarSayWord()
		{
			foreach (PaiMaiAvatar paiMaiAvatar in this.AvatarList)
			{
				if (paiMaiAvatar.State == PaiMaiAvatar.StateType.势在必得)
				{
					int key = SingletonMono<PaiMaiUiMag>.Instance.WordDict[(int)(paiMaiAvatar.State * (PaiMaiAvatar.StateType)10)][Tools.instance.GetRandomInt(0, SingletonMono<PaiMaiUiMag>.Instance.WordDict[(int)(paiMaiAvatar.State * (PaiMaiAvatar.StateType)10)].Count - 1)];
					paiMaiAvatar.UiCtr.SayWord(PaiMaiDuiHuaBiao.DataDict[key].Text);
				}
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x001D482C File Offset: 0x001D2A2C
		public bool IsAllGiveUp()
		{
			using (List<PaiMaiAvatar>.Enumerator enumerator = this.AvatarList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.State != PaiMaiAvatar.StateType.放弃)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x001D4888 File Offset: 0x001D2A88
		public void AddTagetStateMaxPrice(PaiMaiAvatar curAvatar, PaiMaiAvatar.StateType stateType, int lv, float precent)
		{
			foreach (PaiMaiAvatar paiMaiAvatar in this.AvatarList)
			{
				if ((stateType == PaiMaiAvatar.StateType.所有状态 || paiMaiAvatar.State == stateType) && (curAvatar.IsPlayer || curAvatar != paiMaiAvatar) && Tools.instance.GetRandomInt(0, 100) <= lv)
				{
					paiMaiAvatar.AddMaxMoney(precent);
				}
			}
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x001D4908 File Offset: 0x001D2B08
		public void SetCanSelect(CeLueType ceLueType)
		{
			foreach (PaiMaiAvatar paiMaiAvatar in this.AvatarList)
			{
				if (paiMaiAvatar.State != PaiMaiAvatar.StateType.放弃)
				{
					paiMaiAvatar.CanSelect = true;
				}
			}
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x001D4964 File Offset: 0x001D2B64
		public void StopSelect()
		{
			foreach (PaiMaiAvatar paiMaiAvatar in this.AvatarList)
			{
				paiMaiAvatar.CanSelect = false;
			}
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x00030FA1 File Offset: 0x0002F1A1
		private void EndRound()
		{
			SingletonMono<PaiMaiUiMag>.Instance.EndRound();
		}

		// Token: 0x04003C81 RID: 15489
		public List<PaiMaiAvatar> AvatarList;

		// Token: 0x04003C82 RID: 15490
		private int index;
	}
}
