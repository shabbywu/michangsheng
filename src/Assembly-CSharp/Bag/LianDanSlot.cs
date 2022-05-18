using System;
using script.NewLianDan;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000D49 RID: 3401
	public class LianDanSlot : BaseSlot
	{
		// Token: 0x060050D6 RID: 20694 RVA: 0x0003A2D8 File Offset: 0x000384D8
		private void Awake()
		{
			if (!this.IsInBag)
			{
				base.Get<FpBtn>("Null/UnLock").MouseUp = new UnityAction<PointerEventData>(this.OnPointerUp);
			}
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x0003A2FF File Offset: 0x000384FF
		public override void SetSlotData(object data)
		{
			base.SetSlotData(data);
			this.UpdateYaoXin();
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x0021B380 File Offset: 0x00219580
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

		// Token: 0x060050D9 RID: 20697 RVA: 0x0021B420 File Offset: 0x00219620
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

		// Token: 0x060050DA RID: 20698 RVA: 0x0021B494 File Offset: 0x00219694
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

		// Token: 0x060050DB RID: 20699 RVA: 0x0021B4E8 File Offset: 0x002196E8
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

		// Token: 0x060050DC RID: 20700 RVA: 0x0003A30E File Offset: 0x0003850E
		public override void SetNull()
		{
			base.SetNull();
			if (base.name.Contains("主药") || base.name.Contains("辅药"))
			{
				this._yaoXin.SetText("无");
			}
		}

		// Token: 0x04005201 RID: 20993
		public bool IsInBag;

		// Token: 0x04005202 RID: 20994
		public bool IsLock;

		// Token: 0x04005203 RID: 20995
		private Text _yaoXin;

		// Token: 0x04005204 RID: 20996
		private GameObject _lock;

		// Token: 0x04005205 RID: 20997
		private GameObject _unLock;
	}
}
