using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009AA RID: 2474
	[ExecuteInEditMode]
	public class OffsetDraw : EffectDrawObjec
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x0000B311 File Offset: 0x00009511
		public override DrawType type
		{
			get
			{
				return DrawType.Offset;
			}
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x0002D59C File Offset: 0x0002B79C
		protected override void Init()
		{
			this.m_Effects[0] = new OffsetEffect();
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0002D5AB File Offset: 0x0002B7AB
		public void Set(Rect rect)
		{
			this.Set(rect.xMin, rect.yMin, rect.xMax, rect.yMax);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0002D5CF File Offset: 0x0002B7CF
		public void Set(float xMin, float yMin, float xMax, float yMax)
		{
			OffsetEffect offsetEffect = this.m_Effects[0] as OffsetEffect;
			offsetEffect.xMin = xMin;
			offsetEffect.yMin = yMin;
			offsetEffect.xMax = xMax;
			offsetEffect.yMax = yMax;
		}
	}
}
