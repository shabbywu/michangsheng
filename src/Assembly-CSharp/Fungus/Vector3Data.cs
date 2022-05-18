using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001394 RID: 5012
	[Serializable]
	public struct Vector3Data
	{
		// Token: 0x06007943 RID: 31043 RVA: 0x00052C32 File Offset: 0x00050E32
		public Vector3Data(Vector3 v)
		{
			this.vector3Val = v;
			this.vector3Ref = null;
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x00052C42 File Offset: 0x00050E42
		public static implicit operator Vector3(Vector3Data vector3Data)
		{
			return vector3Data.Value;
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06007945 RID: 31045 RVA: 0x00052C4B File Offset: 0x00050E4B
		// (set) Token: 0x06007946 RID: 31046 RVA: 0x00052C6D File Offset: 0x00050E6D
		public Vector3 Value
		{
			get
			{
				if (!(this.vector3Ref == null))
				{
					return this.vector3Ref.Value;
				}
				return this.vector3Val;
			}
			set
			{
				if (this.vector3Ref == null)
				{
					this.vector3Val = value;
					return;
				}
				this.vector3Ref.Value = value;
			}
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x00052C91 File Offset: 0x00050E91
		public string GetDescription()
		{
			if (this.vector3Ref == null)
			{
				return this.vector3Val.ToString();
			}
			return this.vector3Ref.Key;
		}

		// Token: 0x0400690D RID: 26893
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(Vector3Variable)
		})]
		public Vector3Variable vector3Ref;

		// Token: 0x0400690E RID: 26894
		[SerializeField]
		public Vector3 vector3Val;
	}
}
