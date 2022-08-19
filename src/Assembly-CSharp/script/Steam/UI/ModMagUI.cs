using System;
using script.NewLianDan.Base;
using script.Steam.Ctr;
using script.Steam.UI.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.UI
{
	// Token: 0x020009E2 RID: 2530
	public class ModMagUI : BasePanel
	{
		// Token: 0x06004618 RID: 17944 RVA: 0x001DA894 File Offset: 0x001D8A94
		public ModMagUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.Ctr = new ModMagCtr();
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x001DA8B0 File Offset: 0x001D8AB0
		public override void Show()
		{
			if (!this.isInit)
			{
				this.Init();
				this.isInit = true;
			}
			this.CurPage.SetText(string.Format("{0}/{1}", this.Ctr.CurPage, this.Ctr.MaxPage));
			this.Ctr.UpdateList(false);
			WorkShopMag.Inst.ModPoolUI.Show();
			base.Show();
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x001DA928 File Offset: 0x001D8B28
		private void Init()
		{
			base.Get<FpBtn>("刷新按钮").mouseUpEvent.AddListener(delegate()
			{
				this.Ctr.UpdateList(true);
			});
			this.CurPage = base.Get<Text>("翻页/CurPage/Value");
			this.Loading = base.Get("加载中", true);
			base.Get<FpBtn>("翻页/下一页").mouseUpEvent.AddListener(new UnityAction(this.Ctr.AddPage));
			base.Get<FpBtn>("翻页/上一页").mouseUpEvent.AddListener(new UnityAction(this.Ctr.ReducePage));
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x001DA9C5 File Offset: 0x001D8BC5
		public void Select(ModUI modUI)
		{
			if (this.CurSelect != null)
			{
				this.CurSelect.CancelSelect();
			}
			this.CurSelect = modUI;
			WorkShopMag.Inst.MoreModInfoUI.Show(this.CurSelect.Info);
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x001DA9FB File Offset: 0x001D8BFB
		public override void Hide()
		{
			if (this.CurSelect != null)
			{
				this.CurSelect.CancelSelect();
			}
			WorkShopMag.Inst.MoreModInfoUI.Hide();
			this.CurSelect = null;
			this.Ctr.Clear();
			base.Hide();
		}

		// Token: 0x040047A4 RID: 18340
		public ModMagCtr Ctr;

		// Token: 0x040047A5 RID: 18341
		private bool isInit;

		// Token: 0x040047A6 RID: 18342
		public Text CurPage;

		// Token: 0x040047A7 RID: 18343
		public ModUI CurSelect;

		// Token: 0x040047A8 RID: 18344
		public GameObject Loading;
	}
}
