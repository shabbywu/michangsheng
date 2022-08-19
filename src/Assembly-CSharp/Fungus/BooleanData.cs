using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED8 RID: 3800
	[Serializable]
	public struct BooleanData
	{
		// Token: 0x06006B29 RID: 27433 RVA: 0x00295C1B File Offset: 0x00293E1B
		public BooleanData(bool v)
		{
			this.booleanVal = v;
			this.booleanRef = null;
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x00295C2B File Offset: 0x00293E2B
		public static implicit operator bool(BooleanData booleanData)
		{
			return booleanData.Value;
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06006B2B RID: 27435 RVA: 0x00295C34 File Offset: 0x00293E34
		// (set) Token: 0x06006B2C RID: 27436 RVA: 0x00295C56 File Offset: 0x00293E56
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

		// Token: 0x06006B2D RID: 27437 RVA: 0x00295C7A File Offset: 0x00293E7A
		public string GetDescription()
		{
			if (this.booleanRef == null)
			{
				return this.booleanVal.ToString();
			}
			return this.booleanRef.Key;
		}

		// Token: 0x04005A6E RID: 23150
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(BooleanVariable)
		})]
		public BooleanVariable booleanRef;

		// Token: 0x04005A6F RID: 23151
		[SerializeField]
		public bool booleanVal;
	}
}
