using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tab
{
	// Token: 0x020006E9 RID: 1769
	public class BagFilter : MonoBehaviour
	{
		// Token: 0x060038F9 RID: 14585 RVA: 0x001851B0 File Offset: 0x001833B0
		private void Awake()
		{
			this.StopAllAn();
			this.CanSort = true;
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x001851C0 File Offset: 0x001833C0
		public void ResetData()
		{
			this.BigTypeIndex = 0;
			this.CloseSmallSelect();
			foreach (UIInvFilterBtn uiinvFilterBtn in this.BigFilterBtnList)
			{
				uiinvFilterBtn.gameObject.SetActive(false);
			}
			this.CurSelectBigBtn = null;
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x0018522C File Offset: 0x0018342C
		public void AddBigTypeBtn(UnityAction call, string Name)
		{
			if (this.BigTypeIndex >= this.BigTypeMax)
			{
				Debug.LogError("已经超过最大上限");
				return;
			}
			this.BigFilterBtnList[this.BigTypeIndex].ClearListener();
			UIInvFilterBtn BigFilterBtn = this.BigFilterBtnList[this.BigTypeIndex];
			BigFilterBtn.DeafaultName = Name;
			BigFilterBtn.AddClickEvent(delegate
			{
				if (this.CurSelectBigBtn != null)
				{
					this.CloseSmallSelect();
					if (this.CurSelectBigBtn == BigFilterBtn)
					{
						this.StopAllAn();
						this.PlayHideAn();
						this.CurSelectBigBtn = null;
						return;
					}
				}
				this.StopAllAn();
				this.PlayShowAn();
				this.CurSelectBigBtn = BigFilterBtn;
				call.Invoke();
				BigFilterBtn.State = UIInvFilterBtn.BtnState.Normal;
			});
			this.BigFilterBtnList[this.BigTypeIndex].ShowText.SetText(Name);
			this.BigFilterBtnList[this.BigTypeIndex].gameObject.SetActive(true);
			this.BigTypeIndex++;
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x001852FC File Offset: 0x001834FC
		public void AddSmallTypeBtn(UnityAction call, string Name)
		{
			if (this.SmallTypeIndex >= this.SmallTypeMax)
			{
				Debug.LogError("已经超过最大上限");
				return;
			}
			UIInvFilterBtn smallTypeBtn = this.FilterBtnList[this.SmallTypeIndex];
			smallTypeBtn.ClearListener();
			smallTypeBtn.AddClickEvent(delegate
			{
				call.Invoke();
				if (smallTypeBtn.ShowText.text == "全部")
				{
					this.CurSelectBigBtn.ShowText.SetText(this.CurSelectBigBtn.DeafaultName);
				}
				else
				{
					this.CurSelectBigBtn.ShowText.SetText(smallTypeBtn.ShowText.text);
				}
				this.StopAllAn();
				this.PlayHideAn();
				this.CurSelectBigBtn = null;
			});
			if (this.CurSelectBigBtn.ShowText.text == Name)
			{
				smallTypeBtn.State = UIInvFilterBtn.BtnState.Active;
			}
			else
			{
				smallTypeBtn.State = UIInvFilterBtn.BtnState.Normal;
			}
			smallTypeBtn.ShowText.SetText(Name);
			smallTypeBtn.gameObject.SetActive(true);
			this.SmallTypeIndex++;
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x001853D1 File Offset: 0x001835D1
		public void PlayShowAn()
		{
			this.LianZiAnimator.enabled = true;
			this.FilterAnimator.enabled = true;
			this.FilterAnimator.Play("Show");
			this.LianZiAnimator.Play("Show");
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x0018540B File Offset: 0x0018360B
		public void PlayHideAn()
		{
			this.LianZiAnimator.enabled = true;
			this.FilterAnimator.enabled = true;
			this.FilterAnimator.Play("Hide");
			this.LianZiAnimator.Play("Hide");
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x00185445 File Offset: 0x00183645
		public void StopAllAn()
		{
			this.FilterAnimator.enabled = false;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x00185454 File Offset: 0x00183654
		public void CloseSmallSelect()
		{
			foreach (UIInvFilterBtn uiinvFilterBtn in this.FilterBtnList)
			{
				uiinvFilterBtn.gameObject.SetActive(false);
				uiinvFilterBtn.State = UIInvFilterBtn.BtnState.Normal;
			}
			this.SmallTypeIndex = 0;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x001854B8 File Offset: 0x001836B8
		public void Sort(UnityAction action = null)
		{
			if (this.CanSort)
			{
				this.CanSort = false;
				this.PlayShowAn();
				PlayerEx.Player.SortItem();
				if (action != null)
				{
					action.Invoke();
				}
				base.Invoke("SortEnd", 0.5f);
			}
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x001854F2 File Offset: 0x001836F2
		public void SortEnd()
		{
			this.PlayHideAn();
			this.CanSort = true;
		}

		// Token: 0x04003109 RID: 12553
		public Animator FilterAnimator;

		// Token: 0x0400310A RID: 12554
		public Animator LianZiAnimator;

		// Token: 0x0400310B RID: 12555
		public List<UIInvFilterBtn> BigFilterBtnList;

		// Token: 0x0400310C RID: 12556
		public List<UIInvFilterBtn> FilterBtnList;

		// Token: 0x0400310D RID: 12557
		public int BigTypeIndex;

		// Token: 0x0400310E RID: 12558
		public int SmallTypeIndex;

		// Token: 0x0400310F RID: 12559
		public int BigTypeMax = 4;

		// Token: 0x04003110 RID: 12560
		public bool CanSort = true;

		// Token: 0x04003111 RID: 12561
		public int SmallTypeMax = 12;

		// Token: 0x04003112 RID: 12562
		public UIInvFilterBtn CurSelectBigBtn;
	}
}
