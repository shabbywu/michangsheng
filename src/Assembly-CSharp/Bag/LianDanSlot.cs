using System;
using script.NewLianDan;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x020009C0 RID: 2496
	public class LianDanSlot : BaseSlot
	{
		// Token: 0x06004571 RID: 17777 RVA: 0x001D73E0 File Offset: 0x001D55E0
		private void Awake()
		{
			if (!this.IsInBag)
			{
				base.Get<FpBtn>("Null/UnLock").MouseUp = new UnityAction<PointerEventData>(this.OnPointerUp);
			}
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x001D7407 File Offset: 0x001D5607
		public override void SetSlotData(object data)
		{
			base.SetSlotData(data);
			this.UpdateYaoXin();
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x001D7418 File Offset: 0x001D5618
		public void UpdateYaoXin()
		{
			if (this.Item == null)
			{
				if (base.name.Contains("主药") || base.name.Contains("辅药"))
				{
					this._yaoXin.SetText("无");
					return;
				}
			}
			else
			{
				CaoYaoItem caoYaoItem = (CaoYaoItem)this.Item;
				if (base.name.Contains("主药"))
				{
					this._yaoXin.SetText(caoYaoItem.GetZhuYao());
					return;
				}
				if (base.name.Contains("辅药"))
				{
					this._yaoXin.SetText(caoYaoItem.GetFuYao());
				}
			}
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x001D74B8 File Offset: 0x001D56B8
		public override void InitUI()
		{
			base.InitUI();
			if (!this.IsInBag)
			{
				if (base.name.Contains("主药") || base.name.Contains("辅药"))
				{
					this._yaoXin = base.Get<Text>("Bg/药性");
				}
				this._lock = base.Get("Null/Lock", true);
				this._unLock = base.Get("Null/UnLock", true);
			}
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x001D752C File Offset: 0x001D572C
		public void SetIsLock(bool value)
		{
			this.IsLock = value;
			if (this.IsLock)
			{
				this._lock.SetActive(true);
				this._unLock.SetActive(false);
				this.SetNull();
				return;
			}
			this._lock.SetActive(false);
			this._unLock.SetActive(true);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x001D7580 File Offset: 0x001D5780
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (this.IsLock)
			{
				return;
			}
			if (this.IsInBag)
			{
				if (base.IsNull())
				{
					return;
				}
				LianDanUIMag.Instance.LianDanPanel.PutCaoYao(this);
				LianDanUIMag.Instance.CaoYaoBag.Close();
				LianDanUIMag.Instance.LianDanPanel.CheckCanMade();
			}
			else if (eventData.button == null)
			{
				LianDanUIMag.Instance.CaoYaoBag.ToSlot = this;
				if (base.name.Contains("主药"))
				{
					LianDanUIMag.Instance.CaoYaoBag.SelectWeiZhi(1);
				}
				else if (base.name.Contains("辅药"))
				{
					LianDanUIMag.Instance.CaoYaoBag.SelectWeiZhi(2);
				}
				else if (base.name.Contains("药引"))
				{
					LianDanUIMag.Instance.CaoYaoBag.SelectWeiZhi(3);
				}
				LianDanUIMag.Instance.CaoYaoBag.Open();
			}
			else if (eventData.button == 1 && !base.IsNull())
			{
				LianDanUIMag.Instance.LianDanPanel.BackCaoYao(this);
			}
			this._selectPanel.SetActive(false);
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x001D769D File Offset: 0x001D589D
		public override void SetNull()
		{
			base.SetNull();
			if (base.name.Contains("主药") || base.name.Contains("辅药"))
			{
				this._yaoXin.SetText("无");
			}
		}

		// Token: 0x040046FD RID: 18173
		public bool IsInBag;

		// Token: 0x040046FE RID: 18174
		public bool IsLock;

		// Token: 0x040046FF RID: 18175
		private Text _yaoXin;

		// Token: 0x04004700 RID: 18176
		private GameObject _lock;

		// Token: 0x04004701 RID: 18177
		private GameObject _unLock;
	}
}
