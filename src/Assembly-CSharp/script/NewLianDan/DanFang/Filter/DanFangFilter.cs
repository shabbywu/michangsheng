using System;
using System.Collections.Generic;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang.Filter
{
	// Token: 0x02000AD1 RID: 2769
	public class DanFangFilter : BasePanel
	{
		// Token: 0x060046AE RID: 18094 RVA: 0x001E4098 File Offset: 0x001E2298
		public DanFangFilter(GameObject go)
		{
			this._go = go;
			this.TempFilter = base.Get("Mask/Temp", true);
			base.Get<Button>("Bg").onClick.AddListener(new UnityAction(this.Hide));
			this._init = false;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000326D1 File Offset: 0x000308D1
		public override void Show()
		{
			if (!this._init)
			{
				this.Init();
			}
			base.Show();
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x001E40F0 File Offset: 0x001E22F0
		private void Init()
		{
			this.QualityFilterList = new List<QualityFilter>();
			Dictionary<int, string> filterDataDict = LianDanUIMag.Instance.DanFangPanel.FilterDataDict;
			float num = this.TempFilter.transform.localPosition.x;
			float y = this.TempFilter.transform.localPosition.y;
			foreach (int num2 in filterDataDict.Keys)
			{
				QualityFilter qualityFilter = new QualityFilter(this.TempFilter.Inst(this.TempFilter.transform.parent), filterDataDict[num2], num2, num, y);
				qualityFilter.Action = new UnityAction<int>(LianDanUIMag.Instance.DanFangPanel.UpdateFilter);
				this.QualityFilterList.Add(qualityFilter);
				num -= 64f;
			}
			this._init = true;
		}

		// Token: 0x04003EC9 RID: 16073
		public List<QualityFilter> QualityFilterList;

		// Token: 0x04003ECA RID: 16074
		public GameObject TempFilter;

		// Token: 0x04003ECB RID: 16075
		private bool _init;
	}
}
