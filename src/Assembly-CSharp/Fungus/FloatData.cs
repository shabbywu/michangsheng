using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EDC RID: 3804
	[Serializable]
	public struct FloatData
	{
		// Token: 0x06006B3C RID: 27452 RVA: 0x00295FC3 File Offset: 0x002941C3
		public FloatData(float v)
		{
			this.floatVal = v;
			this.floatRef = null;
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x00295FD3 File Offset: 0x002941D3
		public static implicit operator float(FloatData floatData)
		{
			return floatData.Value;
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06006B3E RID: 27454 RVA: 0x00295FDC File Offset: 0x002941DC
		// (set) Token: 0x06006B3F RID: 27455 RVA: 0x00295FFE File Offset: 0x002941FE
		public float Value
		{
			get
			{
				if (!(this.floatRef == null))
				{
					return this.floatRef.Value;
				}
				return this.floatVal;
			}
			set
			{
				if (this.floatRef == null)
				{
					this.floatVal = value;
					return;
				}
				this.floatRef.Value = value;
			}
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x00296022 File Offset: 0x00294222
		public string GetDescription()
		{
			if (this.floatRef == null)
			{
				return this.floatVal.ToString();
			}
			return this.floatRef.Key;
		}

		// Token: 0x04005A76 RID: 23158
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(FloatVariable)
		})]
		public FloatVariable floatRef;

		// Token: 0x04005A77 RID: 23159
		[SerializeField]
		public float floatVal;
	}
}
