using System;
using Fungus;
using UnityEngine;

namespace PaiMai
{
	// Token: 0x02000713 RID: 1811
	public class FungusSay : MonoBehaviour
	{
		// Token: 0x060039FB RID: 14843 RVA: 0x0018D560 File Offset: 0x0018B760
		public void Say(PaiMaiSayData sayData)
		{
			base.gameObject.SetActive(true);
			if (sayData.Action == null)
			{
				sayData.Action = new Action(this.Hide);
			}
			BindData.Bind("PaiMaiSayData", sayData);
			this._flowchart.StopBlock("SayWord");
			this._flowchart.ExecuteBlock("SayWord");
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000B5E62 File Offset: 0x000B4062
		private void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04003204 RID: 12804
		[SerializeField]
		private Flowchart _flowchart;
	}
}
