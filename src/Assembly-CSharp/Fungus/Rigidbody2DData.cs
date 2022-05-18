using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001387 RID: 4999
	[Serializable]
	public struct Rigidbody2DData
	{
		// Token: 0x06007908 RID: 30984 RVA: 0x000526C6 File Offset: 0x000508C6
		public static implicit operator Rigidbody2D(Rigidbody2DData rigidbody2DData)
		{
			return rigidbody2DData.Value;
		}

		// Token: 0x06007909 RID: 30985 RVA: 0x000526CF File Offset: 0x000508CF
		public Rigidbody2DData(Rigidbody2D v)
		{
			this.rigidbody2DVal = v;
			this.rigidbody2DRef = null;
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600790A RID: 30986 RVA: 0x000526DF File Offset: 0x000508DF
		// (set) Token: 0x0600790B RID: 30987 RVA: 0x00052701 File Offset: 0x00050901
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

		// Token: 0x0600790C RID: 30988 RVA: 0x00052725 File Offset: 0x00050925
		public string GetDescription()
		{
			if (this.rigidbody2DRef == null)
			{
				return this.rigidbody2DVal.ToString();
			}
			return this.rigidbody2DRef.Key;
		}

		// Token: 0x040068F3 RID: 26867
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(Rigidbody2DVariable)
		})]
		public Rigidbody2DVariable rigidbody2DRef;

		// Token: 0x040068F4 RID: 26868
		[SerializeField]
		public Rigidbody2D rigidbody2DVal;
	}
}
