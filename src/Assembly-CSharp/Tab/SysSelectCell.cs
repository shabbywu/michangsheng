using System;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

namespace Tab
{
	// Token: 0x020006FE RID: 1790
	[Serializable]
	public class SysSelectCell : UIBase
	{
		// Token: 0x06003976 RID: 14710 RVA: 0x001896B4 File Offset: 0x001878B4
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

		// Token: 0x06003977 RID: 14711 RVA: 0x00189753 File Offset: 0x00187953
		public void SetIsSelect(bool flag)
		{
			this._isActive = flag;
			this.UpdateUI();
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x00189764 File Offset: 0x00187964
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

		// Token: 0x06003979 RID: 14713 RVA: 0x001897C0 File Offset: 0x001879C0
		public void Click()
		{
			if (!this._isActive)
			{
				SingletonMono<TabUIMag>.Instance.SystemPanel.SelectMag.UpdateAll(this);
				MusicMag.instance.PlayEffectMusic(13, 1f);
			}
		}

		// Token: 0x04003191 RID: 12689
		private bool _isActive;

		// Token: 0x04003192 RID: 12690
		private GameObject _unSelect;

		// Token: 0x04003193 RID: 12691
		private GameObject _select;

		// Token: 0x04003194 RID: 12692
		private ISysPanelBase _panel;
	}
}
