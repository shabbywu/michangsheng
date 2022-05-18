using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001385 RID: 4997
	[Serializable]
	public struct ObjectData
	{
		// Token: 0x060078FF RID: 30975 RVA: 0x000525EE File Offset: 0x000507EE
		public ObjectData(Object v)
		{
			this.objectVal = v;
			this.objectRef = null;
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x000525FE File Offset: 0x000507FE
		public static implicit operator Object(ObjectData objectData)
		{
			return objectData.Value;
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06007901 RID: 30977 RVA: 0x00052607 File Offset: 0x00050807
		// (set) Token: 0x06007902 RID: 30978 RVA: 0x00052629 File Offset: 0x00050829
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

		// Token: 0x06007903 RID: 30979 RVA: 0x0005264D File Offset: 0x0005084D
		public string GetDescription()
		{
			if (this.objectRef == null)
			{
				return this.objectVal.ToString();
			}
			return this.objectRef.Key;
		}

		// Token: 0x040068EF RID: 26863
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(ObjectVariable)
		})]
		public ObjectVariable objectRef;

		// Token: 0x040068F0 RID: 26864
		[SerializeField]
		public Object objectVal;
	}
}
