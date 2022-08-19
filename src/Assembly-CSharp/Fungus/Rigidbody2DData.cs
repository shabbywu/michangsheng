using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE6 RID: 3814
	[Serializable]
	public struct Rigidbody2DData
	{
		// Token: 0x06006B69 RID: 27497 RVA: 0x0029667A File Offset: 0x0029487A
		public static implicit operator Rigidbody2D(Rigidbody2DData rigidbody2DData)
		{
			return rigidbody2DData.Value;
		}

		// Token: 0x06006B6A RID: 27498 RVA: 0x00296683 File Offset: 0x00294883
		public Rigidbody2DData(Rigidbody2D v)
		{
			this.rigidbody2DVal = v;
			this.rigidbody2DRef = null;
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06006B6B RID: 27499 RVA: 0x00296693 File Offset: 0x00294893
		// (set) Token: 0x06006B6C RID: 27500 RVA: 0x002966B5 File Offset: 0x002948B5
		public Rigidbody2D Value
		{
			get
			{
				if (!(this.rigidbody2DRef == null))
				{
					return this.rigidbody2DRef.Value;
				}
				return this.rigidbody2DVal;
			}
			set
			{
				if (this.rigidbody2DRef == null)
				{
					this.rigidbody2DVal = value;
					return;
				}
				this.rigidbody2DRef.Value = value;
			}
		}

		// Token: 0x06006B6D RID: 27501 RVA: 0x002966D9 File Offset: 0x002948D9
		public string GetDescription()
		{
			if (this.rigidbody2DRef == null)
			{
				return this.rigidbody2DVal.ToString();
			}
			return this.rigidbody2DRef.Key;
		}

		// Token: 0x04005A8A RID: 23178
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(Rigidbody2DVariable)
		})]
		public Rigidbody2DVariable rigidbody2DRef;

		// Token: 0x04005A8B RID: 23179
		[SerializeField]
		public Rigidbody2D rigidbody2DVal;
	}
}
