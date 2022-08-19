using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000680 RID: 1664
	[ExecuteInEditMode]
	public class AlphaOffsetDraw : OffsetDraw
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x0016F0FD File Offset: 0x0016D2FD
		public override DrawType type
		{
			get
			{
				return DrawType.OffsetAndAlpha;
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x0016F100 File Offset: 0x0016D300
		protected override void Init()
		{
			base.Init();
			this.m_Effects[1] = new AlphaEffect();
		}
	}
}
