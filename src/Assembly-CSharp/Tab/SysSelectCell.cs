using System;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

namespace Tab
{
	// Token: 0x02000A49 RID: 2633
	[Serializable]
	public class SysSelectCell : UIBase
	{
		// Token: 0x060043F3 RID: 17395 RVA: 0x001D0E24 File Offset: 0x001CF024
		public SysSelectCell(GameObject go, ISysPanelBase panel)
		{
			this._go = go;
			this._isActive = false;
			base.Get<FpBtn>("UnSelect").mouseUpEvent.AddListener(new UnityAction(this.Click));
			base.Get<FpBtn>("Select").mouseUpEvent.AddListener(new UnityAction(this.Click));
			this._unSelect = base.Get("UnSelect", true);
			this._select = base.Get("Select", true);
			this._select.gameObject.SetActive(false);
			this._panel = panel;
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000309A3 File Offset: 0x0002EBA3
		public void SetIsSelect(bool flag)
		{
			this._isActive = flag;
			this.UpdateUI();
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x001D0EC4 File Offset: 0x001CF0C4
		private void UpdateUI()
		{
			if (this._isActive)
			{
				this._select.SetActive(true);
				this._unSelect.SetActive(false);
				this._panel.Show();
				return;
			}
			this._unSelect.SetActive(true);
			this._select.SetActive(false);
			this._panel.Hide();
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000309B2 File Offset: 0x0002EBB2
		public void Click()
		{
			if (!this._isActive)
			{
				SingletonMono<TabUIMag>.Instance.SystemPanel.SelectMag.UpdateAll(this);
				MusicMag.instance.PlayEffectMusic(13, 1f);
			}
		}

		// Token: 0x04003C09 RID: 15369
		private bool _isActive;

		// Token: 0x04003C0A RID: 15370
		private GameObject _unSelect;

		// Token: 0x04003C0B RID: 15371
		private GameObject _select;

		// Token: 0x04003C0C RID: 15372
		private ISysPanelBase _panel;
	}
}
