using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EEF RID: 3823
	[Serializable]
	public struct TransformData
	{
		// Token: 0x06006B92 RID: 27538 RVA: 0x00296BDE File Offset: 0x00294DDE
		public TransformData(Transform v)
		{
			this.transformVal = v;
			this.transformRef = null;
		}

		// Token: 0x06006B93 RID: 27539 RVA: 0x00296BEE File Offset: 0x00294DEE
		public static implicit operator Transform(TransformData vector3Data)
		{
			return vector3Data.Value;
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06006B94 RID: 27540 RVA: 0x00296BF7 File Offset: 0x00294DF7
		// (set) Token: 0x06006B95 RID: 27541 RVA: 0x00296C19 File Offset: 0x00294E19
		public Transform Value
		{
			get
			{
				if (!(this.transformRef == null))
				{
					return this.transformRef.Value;
				}
				return this.transformVal;
			}
			set
			{
				if (this.transformRef == null)
				{
					this.transformVal = value;
					return;
				}
				this.transformRef.Value = value;
			}
		}

		// Token: 0x06006B96 RID: 27542 RVA: 0x00296C3D File Offset: 0x00294E3D
		public string GetDescription()
		{
			if (this.transformRef == null)
			{
				return this.transformVal.ToString();
			}
			return this.transformRef.Key;
		}

		// Token: 0x04005A9C RID: 23196
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(TransformVariable)
		})]
		public TransformVariable transformRef;

		// Token: 0x04005A9D RID: 23197
		[SerializeField]
		public Transform transformVal;
	}
}
