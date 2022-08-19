using System;
using System.Collections.Generic;
using script.Steam.Utils;
using Tab;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.UI.Base
{
	// Token: 0x020009E7 RID: 2535
	public class ModUI : UIBase
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600463B RID: 17979 RVA: 0x001DB60A File Offset: 0x001D980A
		// (set) Token: 0x0600463C RID: 17980 RVA: 0x001DB612 File Offset: 0x001D9812
		public bool IsUsed { get; private set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x001DB61B File Offset: 0x001D981B
		// (set) Token: 0x0600463E RID: 17982 RVA: 0x001DB623 File Offset: 0x001D9823
		public ModInfo Info { get; private set; }

		// Token: 0x0600463F RID: 17983 RVA: 0x001DB62C File Offset: 0x001D982C
		public ModUI(GameObject go)
		{
			this.IsUsed = false;
			this._go = go;
			this.modName = base.Get<Text>("Mod名称");
			this.modTag = base.Get<Text>("类型");
			this.modNum = base.Get<Text>("订阅数");
			this.modLv = base.Get<Text>("好评率");
			this.toggle = base.Get<Toggle>("订阅");
			this.select = base.Get("选择UI/已选中", true);
			this.listener = base.Get<UIListener>("选择UI");
			this.listener.mouseUpEvent.AddListener(new UnityAction(this.Click));
			this.listener.mouseEnterEvent.AddListener(delegate()
			{
				this.select.SetActive(true);
			});
			this.listener.mouseOutEvent.AddListener(delegate()
			{
				if (!this.isSelect)
				{
					this.select.SetActive(false);
				}
			});
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x001DB71C File Offset: 0x001D991C
		public void SetType(int type)
		{
			if (type == 1)
			{
				this.modTag.gameObject.SetActive(true);
				this.modNum.gameObject.SetActive(true);
				this.modLv.gameObject.SetActive(true);
			}
			else
			{
				this.modTag.gameObject.SetActive(false);
				this.modNum.gameObject.SetActive(false);
				this.modLv.gameObject.SetActive(false);
			}
			this.type = type;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x001DB79C File Offset: 0x001D999C
		public void BindingInfo(ModInfo modInfo)
		{
			this.Info = modInfo;
			this.IsUsed = true;
			this.toggle.onValueChanged.RemoveAllListeners();
			this.modName.SetTextWithEllipsis(this.Info.Name);
			this.modTag.SetTextWithEllipsis(this.Info.Tags);
			this.modNum.SetText(this.Info.Subscriptions);
			this.modLv.SetText(this.Info.GetLv());
			if (this.type == 0)
			{
				this.toggle.isOn = WorkShopMag.Inst.ModMagUI.Ctr.IsOpen(this.Info.Id);
			}
			else
			{
				this.toggle.isOn = WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(this.Info.Id);
			}
			this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.UpdateToggleState));
			this._go.SetActive(true);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x001DB8AC File Offset: 0x001D9AAC
		private void UpdateToggleState(bool isOn)
		{
			if (this.type == 0)
			{
				if (isOn)
				{
					WorkShopMag.Inst.ModMagUI.Ctr.OpenMod(this.Info.Id);
					WorkShopMag.Inst.IsChange = true;
					return;
				}
				WorkShopMag.Inst.ModMagUI.Ctr.CloseMod(this.Info.Id);
				WorkShopMag.Inst.IsChange = true;
				return;
			}
			else
			{
				if (!isOn)
				{
					WorkShopMag.Inst.WorkShopUI.Ctr.UnSubscriptionMod(this.Info.Id);
					WorkShopMag.Inst.IsChange = true;
					return;
				}
				List<ulong> list = WorkShopMag.Inst.WorkShopUI.Ctr.GetNoSubscriptDependency(this.Info);
				if (list.Count > 0)
				{
					USelectBox.Show("检测到前置Mod未订阅,该Mod无法生效，是否要订阅前置Mod", delegate
					{
						foreach (ulong id in list)
						{
							WorkShopMag.Inst.WorkShopUI.Ctr.SubscriptionMod(id, false);
						}
						WorkShopMag.Inst.WorkShopUI.Ctr.SubscriptionMod(this.Info.Id, true);
						WorkShopMag.Inst.WorkShopUI.Ctr.UpdateList(false);
						WorkShopMag.Inst.IsChange = true;
					}, null);
					return;
				}
				WorkShopMag.Inst.WorkShopUI.Ctr.SubscriptionMod(this.Info.Id, true);
				WorkShopMag.Inst.IsChange = true;
				return;
			}
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x001DB9C6 File Offset: 0x001D9BC6
		public void UnBindingInfo()
		{
			this.Info = null;
			this.IsUsed = false;
			this._go.SetActive(false);
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x001DB9E2 File Offset: 0x001D9BE2
		private void Click()
		{
			if (this.type == 0)
			{
				WorkShopMag.Inst.ModMagUI.Select(this);
			}
			else
			{
				WorkShopMag.Inst.WorkShopUI.Select(this);
			}
			this.select.SetActive(true);
			this.isSelect = true;
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x001DBA21 File Offset: 0x001D9C21
		public void CancelSelect()
		{
			this.select.SetActive(false);
			this.isSelect = false;
			WorkShopMag.Inst.MoreModInfoUI.Hide();
		}

		// Token: 0x040047CD RID: 18381
		private int type;

		// Token: 0x040047CE RID: 18382
		private Text modName;

		// Token: 0x040047CF RID: 18383
		private Text modTag;

		// Token: 0x040047D0 RID: 18384
		private Text modNum;

		// Token: 0x040047D1 RID: 18385
		private Text modLv;

		// Token: 0x040047D2 RID: 18386
		private Toggle toggle;

		// Token: 0x040047D3 RID: 18387
		private GameObject select;

		// Token: 0x040047D4 RID: 18388
		private UIListener listener;

		// Token: 0x040047D5 RID: 18389
		private bool isSelect;
	}
}
