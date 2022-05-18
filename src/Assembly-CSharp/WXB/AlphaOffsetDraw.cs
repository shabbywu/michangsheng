using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200098D RID: 2445
	[ExecuteInEditMode]
	public class AlphaOffsetDraw : OffsetDraw
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06003E7D RID: 15997 RVA: 0x0002D050 File Offset: 0x0002B250
		public override DrawType type
		{
			get
			{
				return DrawType.OffsetAndAlpha;
			}
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x0002D053 File Offset: 0x0002B253
		protected override void Init()
		{
			base.Init();
			this.m_Effects[1] = new AlphaEffect();
		}
	}
}
