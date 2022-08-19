using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200068D RID: 1677
	[ExecuteInEditMode]
	public class EffectDrawObjec : DrawObject
	{
		// Token: 0x06003520 RID: 13600 RVA: 0x00170034 File Offset: 0x0016E234
		public override void UpdateSelf(float deltaTime)
		{
			for (int i = 0; i < this.m_Effects.Length; i++)
			{
				if (this.m_Effects[i] != null)
				{
					this.m_Effects[i].UpdateEffect(this, deltaTime);
				}
			}
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x0017006D File Offset: 0x0016E26D
		protected bool GetOpen(int index)
		{
			return this.m_Effects[index] != null;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x0017007A File Offset: 0x0016E27A
		protected void SetOpen<T>(int index, bool value) where T : IEffect, new()
		{
			if (this.GetOpen(index) == value)
			{
				return;
			}
			if (value)
			{
				this.m_Effects[index] = Activator.CreateInstance<T>();
				return;
			}
			this.m_Effects[index] = null;
		}

		// Token: 0x04002EEA RID: 12010
		protected IEffect[] m_Effects = new IEffect[2];
	}
}
