using System;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000AAF RID: 2735
	public class TuJianMapTab : TuJianTab
	{
		// Token: 0x06004CB2 RID: 19634 RVA: 0x0020D07E File Offset: 0x0020B27E
		public override void Awake()
		{
			TuJianMapTab.Inst = this;
			this.TabType = TuJianTabType.Map;
			base.Awake();
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x00004095 File Offset: 0x00002295
		private void Update()
		{
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0020D093 File Offset: 0x0020B293
		public override void Show()
		{
			base.Show();
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0020CB8E File Offset: 0x0020AD8E
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x04004BCC RID: 19404
		[HideInInspector]
		public static TuJianMapTab Inst;
	}
}
