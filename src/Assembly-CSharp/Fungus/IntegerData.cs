using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001381 RID: 4993
	[Serializable]
	public struct IntegerData
	{
		// Token: 0x060078ED RID: 30957 RVA: 0x0005243E File Offset: 0x0005063E
		public IntegerData(int v)
		{
			this.integerVal = v;
			this.integerRef = null;
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x0005244E File Offset: 0x0005064E
		public static implicit operator int(IntegerData integerData)
		{
			return integerData.Value;
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060078EF RID: 30959 RVA: 0x00052457 File Offset: 0x00050657
		// (set) Token: 0x060078F0 RID: 30960 RVA: 0x00052479 File Offset: 0x00050679
		public int Value
		{
			get
			{
				if (!(this.integerRef == null))
				{
					return this.integerRef.Value;
				}
				return this.integerVal;
			}
			set
			{
				if (this.integerRef == null)
				{
					this.integerVal = value;
					return;
				}
				this.integerRef.Value = value;
			}
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x0005249D File Offset: 0x0005069D
		public string GetDescription()
		{
			if (this.integerRef == null)
			{
				return this.integerVal.ToString();
			}
			return this.integerRef.Key;
		}

		// Token: 0x040068E7 RID: 26855
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(IntegerVariable)
		})]
		public IntegerVariable integerRef;

		// Token: 0x040068E8 RID: 26856
		[SerializeField]
		public int integerVal;
	}
}
