using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001379 RID: 4985
	[Serializable]
	public struct BooleanData
	{
		// Token: 0x060078C8 RID: 30920 RVA: 0x00052120 File Offset: 0x00050320
		public BooleanData(bool v)
		{
			this.booleanVal = v;
			this.booleanRef = null;
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x00052130 File Offset: 0x00050330
		public static implicit operator bool(BooleanData booleanData)
		{
			return booleanData.Value;
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060078CA RID: 30922 RVA: 0x00052139 File Offset: 0x00050339
		// (set) Token: 0x060078CB RID: 30923 RVA: 0x0005215B File Offset: 0x0005035B
		public bool Value
		{
			get
			{
				if (!(this.booleanRef == null))
				{
					return this.booleanRef.Value;
				}
				return this.booleanVal;
			}
			set
			{
				if (this.booleanRef == null)
				{
					this.booleanVal = value;
					return;
				}
				this.booleanRef.Value = value;
			}
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x0005217F File Offset: 0x0005037F
		public string GetDescription()
		{
			if (this.booleanRef == null)
			{
				return this.booleanVal.ToString();
			}
			return this.booleanRef.Key;
		}

		// Token: 0x040068D7 RID: 26839
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(BooleanVariable)
		})]
		public BooleanVariable booleanRef;

		// Token: 0x040068D8 RID: 26840
		[SerializeField]
		public bool booleanVal;
	}
}
