using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab
{
	// Token: 0x02000A54 RID: 2644
	[Serializable]
	public class TabSelectCell : UIBase
	{
		// Token: 0x06004436 RID: 17462 RVA: 0x001D26EC File Offset: 0x001D08EC
		public TabSelectCell(GameObject go, ITabPanelBase panel)
		{
			this._go = go;
			this._go.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Click));
			this._isActive = false;
			this._unSelect = base.Get("UnSelect", true);
			this._select = base.Get("Select", true);
			this._select.gameObject.SetActive(false);
			this._panel = panel;
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00030D6C File Offset: 0x0002EF6C
		public void SetIsSelect(bool flag)
		{
			this._isActive = flag;
			this.UpdateUI();
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x001D276C File Offset: 0x001D096C
		private void UpdateUI()
		{
			if (!this._isActive)
			{
				this._unSelect.SetActive(true);
				this._select.SetActive(false);
				this._panel.Hide();
				return;
			}
			this._select.SetActive(true);
			this._unSelect.SetActive(false);
			this._panel.Show();
			if (this._panel.HasHp)
			{
				SingletonMono<TabUIMag>.Instance.ShowBaseData();
				return;
			}
			SingletonMono<TabUIMag>.Instance.HideBaseData();
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x001D27EC File Offset: 0x001D09EC
		public void Click()
		{
			if (!this._isActive)
			{
				SingletonMono<TabUIMag>.Instance.TabBag.Close();
				SingletonMono<TabUIMag>.Instance.TabFangAnPanel.Close();
				SingletonMono<TabUIMag>.Instance.TabSelect.UpdateAll(this);
				MusicMag.instance.PlayEffectMusic(13, 1f);
			}
		}

		// Token: 0x04003C3F RID: 15423
		private bool _isActive;

		// Token: 0x04003C40 RID: 15424
		private GameObject _unSelect;

		// Token: 0x04003C41 RID: 15425
		private GameObject _select;

		// Token: 0x04003C42 RID: 15426
		private ITabPanelBase _panel;
	}
}
