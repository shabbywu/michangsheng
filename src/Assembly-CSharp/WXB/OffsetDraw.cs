using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000696 RID: 1686
	[ExecuteInEditMode]
	public class OffsetDraw : EffectDrawObjec
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x00031A1A File Offset: 0x0002FC1A
		public override DrawType type
		{
			get
			{
				return DrawType.Offset;
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x001707B5 File Offset: 0x0016E9B5
		protected override void Init()
		{
			this.m_Effects[0] = new OffsetEffect();
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x001707C4 File Offset: 0x0016E9C4
		public void Set(Rect rect)
		{
			this.Set(rect.xMin, rect.yMin, rect.xMax, rect.yMax);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x001707E8 File Offset: 0x0016E9E8
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
