using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200137D RID: 4989
	[Serializable]
	public struct FloatData
	{
		// Token: 0x060078DB RID: 30939 RVA: 0x000522AA File Offset: 0x000504AA
		public FloatData(float v)
		{
			this.floatVal = v;
			this.floatRef = null;
		}

		// Token: 0x060078DC RID: 30940 RVA: 0x000522BA File Offset: 0x000504BA
		public static implicit operator float(FloatData floatData)
		{
			return floatData.Value;
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060078DD RID: 30941 RVA: 0x000522C3 File Offset: 0x000504C3
		// (set) Token: 0x060078DE RID: 30942 RVA: 0x000522E5 File Offset: 0x000504E5
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

		// Token: 0x060078DF RID: 30943 RVA: 0x00052309 File Offset: 0x00050509
		public string GetDescription()
		{
			if (this.floatRef == null)
			{
				return this.floatVal.ToString();
			}
			return this.floatRef.Key;
		}

		// Token: 0x040068DF RID: 26847
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(FloatVariable)
		})]
		public FloatVariable floatRef;

		// Token: 0x040068E0 RID: 26848
		[SerializeField]
		public float floatVal;
	}
}
