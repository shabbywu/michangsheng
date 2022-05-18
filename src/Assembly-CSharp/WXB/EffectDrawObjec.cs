using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200099F RID: 2463
	[ExecuteInEditMode]
	public class EffectDrawObjec : DrawObject
	{
		// Token: 0x06003EDB RID: 16091 RVA: 0x001B83AC File Offset: 0x001B65AC
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

		// Token: 0x06003EDC RID: 16092 RVA: 0x0002D3A3 File Offset: 0x0002B5A3
		protected bool GetOpen(int index)
		{
			return this.m_Effects[index] != null;
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x0002D3B0 File Offset: 0x0002B5B0
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

		// Token: 0x0400389D RID: 14493
		protected IEffect[] m_Effects = new IEffect[2];
	}
}
