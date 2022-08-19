using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai
{
	// Token: 0x02000710 RID: 1808
	public class AvatarCtr : MonoBehaviour
	{
		// Token: 0x060039E6 RID: 14822 RVA: 0x0018CE70 File Offset: 0x0018B070
		public void AllAvatarThinkItem()
		{
			foreach (PaiMaiAvatar paiMaiAvatar in this.AvatarList)
			{
				paiMaiAvatar.ThinKCurShop();
			}
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x0018CEC0 File Offset: 0x0018B0C0
		public void AvatarStart()
		{
			this.index = 0;
			base.Invoke("AvatarAddPrice", 0.75f);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x0018CEDC File Offset: 0x0018B0DC
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

		// Token: 0x060039E9 RID: 14825 RVA: 0x0018CFC0 File Offset: 0x0018B1C0
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

		// Token: 0x060039EA RID: 14826 RVA: 0x0018D07C File Offset: 0x0018B27C
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

		// Token: 0x060039EB RID: 14827 RVA: 0x0018D0D8 File Offset: 0x0018B2D8
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

		// Token: 0x060039EC RID: 14828 RVA: 0x0018D158 File Offset: 0x0018B358
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

		// Token: 0x060039ED RID: 14829 RVA: 0x0018D1B4 File Offset: 0x0018B3B4
		public void StopSelect()
		{
			foreach (PaiMaiAvatar paiMaiAvatar in this.AvatarList)
			{
				paiMaiAvatar.CanSelect = false;
			}
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x0018D208 File Offset: 0x0018B408
		private void EndRound()
		{
			SingletonMono<PaiMaiUiMag>.Instance.EndRound();
		}

		// Token: 0x040031FA RID: 12794
		public List<PaiMaiAvatar> AvatarList;

		// Token: 0x040031FB RID: 12795
		private int index;
	}
}
