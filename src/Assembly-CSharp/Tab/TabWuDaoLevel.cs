using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A31 RID: 2609
	[Serializable]
	public class TabWuDaoLevel : UIBase
	{
		// Token: 0x06004387 RID: 17287 RVA: 0x00030467 File Offset: 0x0002E667
		public TabWuDaoLevel(GameObject go, int id)
		{
			this._go = go;
			this._wudaoExpText = base.Get<Text>("BaseLevel/WuDaoValue");
			this._slider = base.Get<Image>("Slider/JinDu");
			this.UpdateUI(id);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x001CD6A4 File Offset: 0x001CB8A4
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

		// Token: 0x04003B86 RID: 15238
		private List<TabWuDaoLevelBase> levelList;

		// Token: 0x04003B87 RID: 15239
		public int CurExp;

		// Token: 0x04003B88 RID: 15240
		private Text _wudaoExpText;

		// Token: 0x04003B89 RID: 15241
		private Image _slider;
	}
}
