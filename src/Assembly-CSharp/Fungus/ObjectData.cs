using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE4 RID: 3812
	[Serializable]
	public struct ObjectData
	{
		// Token: 0x06006B60 RID: 27488 RVA: 0x0029654A File Offset: 0x0029474A
		public ObjectData(Object v)
		{
			this.objectVal = v;
			this.objectRef = null;
		}

		// Token: 0x06006B61 RID: 27489 RVA: 0x0029655A File Offset: 0x0029475A
		public static implicit operator Object(ObjectData objectData)
		{
			return objectData.Value;
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06006B62 RID: 27490 RVA: 0x00296563 File Offset: 0x00294763
		// (set) Token: 0x06006B63 RID: 27491 RVA: 0x00296585 File Offset: 0x00294785
		public Object Value
		{
			get
			{
				if (!(this.objectRef == null))
				{
					return this.objectRef.Value;
				}
				return this.objectVal;
			}
			set
			{
				if (this.objectRef == null)
				{
					this.objectVal = value;
					return;
				}
				this.objectRef.Value = value;
			}
		}

		// Token: 0x06006B64 RID: 27492 RVA: 0x002965A9 File Offset: 0x002947A9
		public string GetDescription()
		{
			if (this.objectRef == null)
			{
				return this.objectVal.ToString();
			}
			return this.objectRef.Key;
		}

		// Token: 0x04005A86 RID: 23174
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(ObjectVariable)
		})]
		public ObjectVariable objectRef;

		// Token: 0x04005A87 RID: 23175
		[SerializeField]
		public Object objectVal;
	}
}
