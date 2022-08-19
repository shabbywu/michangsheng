using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab
{
	// Token: 0x02000707 RID: 1799
	[Serializable]
	public class TabSelectCell : UIBase
	{
		// Token: 0x060039B2 RID: 14770 RVA: 0x0018AF88 File Offset: 0x00189188
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

		// Token: 0x060039B3 RID: 14771 RVA: 0x0018B006 File Offset: 0x00189206
		public void SetIsSelect(bool flag)
		{
			this._isActive = flag;
			this.UpdateUI();
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x0018B018 File Offset: 0x00189218
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

		// Token: 0x060039B5 RID: 14773 RVA: 0x0018B098 File Offset: 0x00189298
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

		// Token: 0x040031C3 RID: 12739
		private bool _isActive;

		// Token: 0x040031C4 RID: 12740
		private GameObject _unSelect;

		// Token: 0x040031C5 RID: 12741
		private GameObject _select;

		// Token: 0x040031C6 RID: 12742
		private ITabPanelBase _panel;
	}
}
