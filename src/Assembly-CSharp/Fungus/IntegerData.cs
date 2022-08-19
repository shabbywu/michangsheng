using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE0 RID: 3808
	[Serializable]
	public struct IntegerData
	{
		// Token: 0x06006B4E RID: 27470 RVA: 0x002962E7 File Offset: 0x002944E7
		public IntegerData(int v)
		{
			this.integerVal = v;
			this.integerRef = null;
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x002962F7 File Offset: 0x002944F7
		public static implicit operator int(IntegerData integerData)
		{
			return integerData.Value;
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06006B50 RID: 27472 RVA: 0x00296300 File Offset: 0x00294500
		// (set) Token: 0x06006B51 RID: 27473 RVA: 0x00296322 File Offset: 0x00294522
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

		// Token: 0x06006B52 RID: 27474 RVA: 0x00296346 File Offset: 0x00294546
		public string GetDescription()
		{
			if (this.integerRef == null)
			{
				return this.integerVal.ToString();
			}
			return this.integerRef.Key;
		}

		// Token: 0x04005A7E RID: 23166
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(IntegerVariable)
		})]
		public IntegerVariable integerRef;

		// Token: 0x04005A7F RID: 23167
		[SerializeField]
		public int integerVal;
	}
}
