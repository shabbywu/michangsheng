using System;
using Fungus;
using UnityEngine;

namespace PaiMai
{
	// Token: 0x02000A63 RID: 2659
	public class FungusSay : MonoBehaviour
	{
		// Token: 0x06004487 RID: 17543 RVA: 0x001D4C5C File Offset: 0x001D2E5C
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

		// Token: 0x06004488 RID: 17544 RVA: 0x00017C2D File Offset: 0x00015E2D
		private void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04003C8B RID: 15499
		[SerializeField]
		private Flowchart _flowchart;
	}
}
