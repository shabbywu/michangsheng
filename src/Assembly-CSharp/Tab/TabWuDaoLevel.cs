using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006F0 RID: 1776
	[Serializable]
	public class TabWuDaoLevel : UIBase
	{
		// Token: 0x0600391C RID: 14620 RVA: 0x00185CA0 File Offset: 0x00183EA0
		public TabWuDaoLevel(GameObject go, int id)
		{
			this._go = go;
			this._wudaoExpText = base.Get<Text>("BaseLevel/WuDaoValue");
			this._slider = base.Get<Image>("Slider/JinDu");
			this.UpdateUI(id);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x00185CD8 File Offset: 0x00183ED8
		public void UpdateUI(int id)
		{
			this.CurExp = Tools.instance.getPlayer().wuDaoMag.getWuDaoEx(id).I;
			this._slider.fillAmount = Tools.instance.getPlayer().wuDaoMag.getWuDaoExPercent(id) / 100f;
			this._wudaoExpText.text = this.CurExp.ToString();
			if (this.levelList != null)
			{
				this.levelList.Clear();
			}
			this.levelList = new List<TabWuDaoLevelBase>();
			for (int i = 1; i <= 5; i++)
			{
				this.levelList.Add(new TabWuDaoLevelBase(base.Get(string.Format("Level{0}", i), true), id, i));
			}
		}

		// Token: 0x04003121 RID: 12577
		private List<TabWuDaoLevelBase> levelList;

		// Token: 0x04003122 RID: 12578
		public int CurExp;

		// Token: 0x04003123 RID: 12579
		private Text _wudaoExpText;

		// Token: 0x04003124 RID: 12580
		private Image _slider;
	}
}
