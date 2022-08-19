using System;
using System.Collections.Generic;
using script.NewLianDan.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang.Filter
{
	// Token: 0x02000A02 RID: 2562
	public class DanFangFilter : BasePanel
	{
		// Token: 0x060046FE RID: 18174 RVA: 0x001E1C48 File Offset: 0x001DFE48
		public DanFangFilter(GameObject go)
		{
			this._go = go;
			this.TempFilter = base.Get("Mask/Temp", true);
			base.Get<Button>("Bg").onClick.AddListener(new UnityAction(this.Hide));
			this._init = false;
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x001E1C9D File Offset: 0x001DFE9D
		public override void Show()
		{
			if (!this._init)
			{
				this.Init();
			}
			base.Show();
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x001E1CB4 File Offset: 0x001DFEB4
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

		// Token: 0x0400484A RID: 18506
		public List<QualityFilter> QualityFilterList;

		// Token: 0x0400484B RID: 18507
		public GameObject TempFilter;

		// Token: 0x0400484C RID: 18508
		private bool _init;
	}
}
