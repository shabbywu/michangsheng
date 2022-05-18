using System;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab
{
	// Token: 0x02000A34 RID: 2612
	[Serializable]
	public class TabWuDaoToggle : UIBase
	{
		// Token: 0x06004395 RID: 17301 RVA: 0x001CE100 File Offset: 0x001CC300
		public TabWuDaoToggle(GameObject go, int wudaoId, Sprite sprite)
		{
			this._go = go;
			this._isActive = false;
			this.Id = wudaoId;
			this._active = base.Get("Active", true);
			this._noActive = base.Get("NoActive", true);
			WuDaoAllTypeJson wuDaoAllTypeJson = WuDaoAllTypeJson.DataDict[wudaoId];
			TabListener tabListener = go.AddComponent<TabListener>();
			tabListener.mouseEnterEvent.AddListener(new UnityAction(this.Enter));
			tabListener.mouseOutEvent.AddListener(new UnityAction(this.Out));
			tabListener.mouseUpEvent.AddListener(new UnityAction(this.Click));
			base.Get<Image>("Active/Icon").sprite = sprite;
			base.Get<Image>("NoActive/Icon").sprite = sprite;
			base.Get<Text>("Active/Name").text = wuDaoAllTypeJson.name;
			base.Get<Text>("NoActive/Name").text = wuDaoAllTypeJson.name;
			this._go.SetActive(true);
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000304F9 File Offset: 0x0002E6F9
		private void Click()
		{
			if (!this._isActive)
			{
				SingletonMono<TabUIMag>.Instance.WuDaoPanel.SelectTypeCallBack(this);
				MusicMag.instance.PlayEffectMusic(1, 1f);
			}
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x00030523 File Offset: 0x0002E723
		public void SetIsActive(bool flag)
		{
			this._isActive = flag;
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x0003052C File Offset: 0x0002E72C
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

		// Token: 0x06004399 RID: 17305 RVA: 0x00030567 File Offset: 0x0002E767
		private void Enter()
		{
			if (!this._isActive)
			{
				this._noActive.SetActive(false);
				this._active.SetActive(true);
			}
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x00030589 File Offset: 0x0002E789
		private void Out()
		{
			this.UpdateUI();
		}

		// Token: 0x04003B99 RID: 15257
		private bool _isActive;

		// Token: 0x04003B9A RID: 15258
		private GameObject _active;

		// Token: 0x04003B9B RID: 15259
		private GameObject _noActive;

		// Token: 0x04003B9C RID: 15260
		public int Id;
	}
}
