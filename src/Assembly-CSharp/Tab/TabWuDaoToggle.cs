using System;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab
{
	// Token: 0x020006F3 RID: 1779
	[Serializable]
	public class TabWuDaoToggle : UIBase
	{
		// Token: 0x0600392A RID: 14634 RVA: 0x00186790 File Offset: 0x00184990
		public TabWuDaoToggle(GameObject go, int wudaoId, Sprite sprite)
		{
			this._go = go;
			this._isActive = false;
			this.Id = wudaoId;
			this._active = base.Get("Active", true);
			this._noActive = base.Get("NoActive", true);
			WuDaoAllTypeJson wuDaoAllTypeJson = WuDaoAllTypeJson.DataDict[wudaoId];
			UIListener uilistener = go.AddComponent<UIListener>();
			uilistener.mouseEnterEvent.AddListener(new UnityAction(this.Enter));
			uilistener.mouseOutEvent.AddListener(new UnityAction(this.Out));
			uilistener.mouseUpEvent.AddListener(new UnityAction(this.Click));
			base.Get<Image>("Active/Icon").sprite = sprite;
			base.Get<Image>("NoActive/Icon").sprite = sprite;
			base.Get<Text>("Active/Name").text = wuDaoAllTypeJson.name;
			base.Get<Text>("NoActive/Name").text = wuDaoAllTypeJson.name;
			this._go.SetActive(true);
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x0018688C File Offset: 0x00184A8C
		private void Click()
		{
			if (!this._isActive)
			{
				SingletonMono<TabUIMag>.Instance.WuDaoPanel.SelectTypeCallBack(this);
				MusicMag.instance.PlayEffectMusic(1, 1f);
			}
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x001868B6 File Offset: 0x00184AB6
		public void SetIsActive(bool flag)
		{
			this._isActive = flag;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x001868BF File Offset: 0x00184ABF
		public void UpdateUI()
		{
			if (this._isActive)
			{
				this._noActive.SetActive(false);
				this._active.SetActive(true);
				return;
			}
			this._noActive.SetActive(true);
			this._active.SetActive(false);
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x001868FA File Offset: 0x00184AFA
		private void Enter()
		{
			if (!this._isActive)
			{
				this._noActive.SetActive(false);
				this._active.SetActive(true);
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x0018691C File Offset: 0x00184B1C
		private void Out()
		{
			this.UpdateUI();
		}

		// Token: 0x04003134 RID: 12596
		private bool _isActive;

		// Token: 0x04003135 RID: 12597
		private GameObject _active;

		// Token: 0x04003136 RID: 12598
		private GameObject _noActive;

		// Token: 0x04003137 RID: 12599
		public int Id;
	}
}
