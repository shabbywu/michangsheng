using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001390 RID: 5008
	[Serializable]
	public struct TransformData
	{
		// Token: 0x06007931 RID: 31025 RVA: 0x00052AC8 File Offset: 0x00050CC8
		public TransformData(Transform v)
		{
			this.transformVal = v;
			this.transformRef = null;
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x00052AD8 File Offset: 0x00050CD8
		public static implicit operator Transform(TransformData vector3Data)
		{
			return vector3Data.Value;
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06007933 RID: 31027 RVA: 0x00052AE1 File Offset: 0x00050CE1
		// (set) Token: 0x06007934 RID: 31028 RVA: 0x00052B03 File Offset: 0x00050D03
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

		// Token: 0x06007935 RID: 31029 RVA: 0x00052B27 File Offset: 0x00050D27
		public string GetDescription()
		{
			if (this.transformRef == null)
			{
				return this.transformVal.ToString();
			}
			return this.transformRef.Key;
		}

		// Token: 0x04006905 RID: 26885
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(TransformVariable)
		})]
		public TransformVariable transformRef;

		// Token: 0x04006906 RID: 26886
		[SerializeField]
		public Transform transformVal;
	}
}
