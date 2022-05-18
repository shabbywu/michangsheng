using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200098B RID: 2443
	[ExecuteInEditMode]
	public class AlphaDraw : EffectDrawObjec
	{
		// Token: 0x06003E76 RID: 15990 RVA: 0x0002CFD1 File Offset: 0x0002B1D1
		protected override void Init()
		{
			this.m_Effects[0] = new AlphaEffect();
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06003E77 RID: 15991 RVA: 0x0000A093 File Offset: 0x00008293
		public override DrawType type
		{
			get
			{
				return DrawType.Alpha;
			}
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
		public override void Release()
		{
			base.Release();
			this.m_Effects[0].Release();
		}
	}
}
