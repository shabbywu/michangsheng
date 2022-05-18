using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001383 RID: 4995
	[Serializable]
	public struct MaterialData
	{
		// Token: 0x060078F6 RID: 30966 RVA: 0x00052516 File Offset: 0x00050716
		public MaterialData(Material v)
		{
			this.materialVal = v;
			this.materialRef = null;
		}

		// Token: 0x060078F7 RID: 30967 RVA: 0x00052526 File Offset: 0x00050726
		public static implicit operator Material(MaterialData materialData)
		{
			return materialData.Value;
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060078F8 RID: 30968 RVA: 0x0005252F File Offset: 0x0005072F
		// (set) Token: 0x060078F9 RID: 30969 RVA: 0x00052551 File Offset: 0x00050751
		public Material Value
		{
			get
			{
				if (!(this.materialRef == null))
				{
					return this.materialRef.Value;
				}
				return this.materialVal;
			}
			set
			{
				if (this.materialRef == null)
				{
					this.materialVal = value;
					return;
				}
				this.materialRef.Value = value;
			}
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x00052575 File Offset: 0x00050775
		public string GetDescription()
		{
			if (this.materialRef == null)
			{
				return this.materialVal.ToString();
			}
			return this.materialRef.Key;
		}

		// Token: 0x040068EB RID: 26859
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(MaterialVariable)
		})]
		public MaterialVariable materialRef;

		// Token: 0x040068EC RID: 26860
		[SerializeField]
		public Material materialVal;
	}
}
