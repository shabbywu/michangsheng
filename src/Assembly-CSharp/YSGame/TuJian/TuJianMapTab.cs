using System;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DEC RID: 3564
	public class TuJianMapTab : TuJianTab
	{
		// Token: 0x060055FF RID: 22015 RVA: 0x0003D873 File Offset: 0x0003BA73
		public override void Awake()
		{
			TuJianMapTab.Inst = this;
			this.TabType = TuJianTabType.Map;
			base.Awake();
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x000042DD File Offset: 0x000024DD
		private void Update()
		{
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x0003D888 File Offset: 0x0003BA88
		public override void Show()
		{
			base.Show();
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x0003D828 File Offset: 0x0003BA28
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x040055AA RID: 21930
		[HideInInspector]
		public static TuJianMapTab Inst;
	}
}
