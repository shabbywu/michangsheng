using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200067E RID: 1662
	[ExecuteInEditMode]
	public class AlphaDraw : EffectDrawObjec
	{
		// Token: 0x060034C6 RID: 13510 RVA: 0x0016EFA4 File Offset: 0x0016D1A4
		protected override void Init()
		{
			this.m_Effects[0] = new AlphaEffect();
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override DrawType type
		{
			get
			{
				return DrawType.Alpha;
			}
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x0016EFB3 File Offset: 0x0016D1B3
		public override void Release()
		{
			base.Release();
			this.m_Effects[0].Release();
		}
	}
}
