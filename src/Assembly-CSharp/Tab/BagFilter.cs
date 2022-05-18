using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tab
{
	// Token: 0x02000A28 RID: 2600
	public class BagFilter : MonoBehaviour
	{
		// Token: 0x06004360 RID: 17248 RVA: 0x00030251 File Offset: 0x0002E451
		private void Awake()
		{
			this.StopAllAn();
			this.CanSort = true;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x001CCC84 File Offset: 0x001CAE84
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

		// Token: 0x06004362 RID: 17250 RVA: 0x001CCCF0 File Offset: 0x001CAEF0
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

		// Token: 0x06004363 RID: 17251 RVA: 0x001CCDC0 File Offset: 0x001CAFC0
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

		// Token: 0x06004364 RID: 17252 RVA: 0x00030260 File Offset: 0x0002E460
		public void PlayShowAn()
		{
			this.LianZiAnimator.enabled = true;
			this.FilterAnimator.enabled = true;
			this.FilterAnimator.Play("Show");
			this.LianZiAnimator.Play("Show");
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x0003029A File Offset: 0x0002E49A
		public void PlayHideAn()
		{
			this.LianZiAnimator.enabled = true;
			this.FilterAnimator.enabled = true;
			this.FilterAnimator.Play("Hide");
			this.LianZiAnimator.Play("Hide");
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000302D4 File Offset: 0x0002E4D4
		public void StopAllAn()
		{
			this.FilterAnimator.enabled = false;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x001CCE98 File Offset: 0x001CB098
		public void CloseSmallSelect()
		{
			foreach (UIInvFilterBtn uiinvFilterBtn in this.FilterBtnList)
			{
				uiinvFilterBtn.gameObject.SetActive(false);
				uiinvFilterBtn.State = UIInvFilterBtn.BtnState.Normal;
			}
			this.SmallTypeIndex = 0;
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000302E2 File Offset: 0x0002E4E2
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

		// Token: 0x06004369 RID: 17257 RVA: 0x0003031C File Offset: 0x0002E51C
		public void SortEnd()
		{
			this.PlayHideAn();
			this.CanSort = true;
		}

		// Token: 0x04003B68 RID: 15208
		public Animator FilterAnimator;

		// Token: 0x04003B69 RID: 15209
		public Animator LianZiAnimator;

		// Token: 0x04003B6A RID: 15210
		public List<UIInvFilterBtn> BigFilterBtnList;

		// Token: 0x04003B6B RID: 15211
		public List<UIInvFilterBtn> FilterBtnList;

		// Token: 0x04003B6C RID: 15212
		public int BigTypeIndex;

		// Token: 0x04003B6D RID: 15213
		public int SmallTypeIndex;

		// Token: 0x04003B6E RID: 15214
		public int BigTypeMax = 4;

		// Token: 0x04003B6F RID: 15215
		public bool CanSort = true;

		// Token: 0x04003B70 RID: 15216
		public int SmallTypeMax = 12;

		// Token: 0x04003B71 RID: 15217
		public UIInvFilterBtn CurSelectBigBtn;
	}
}
