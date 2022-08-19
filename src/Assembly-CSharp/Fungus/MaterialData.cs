using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE2 RID: 3810
	[Serializable]
	public struct MaterialData
	{
		// Token: 0x06006B57 RID: 27479 RVA: 0x0029641A File Offset: 0x0029461A
		public MaterialData(Material v)
		{
			this.materialVal = v;
			this.materialRef = null;
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x0029642A File Offset: 0x0029462A
		public static implicit operator Material(MaterialData materialData)
		{
			return materialData.Value;
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06006B59 RID: 27481 RVA: 0x00296433 File Offset: 0x00294633
		// (set) Token: 0x06006B5A RID: 27482 RVA: 0x00296455 File Offset: 0x00294655
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

		// Token: 0x06006B5B RID: 27483 RVA: 0x00296479 File Offset: 0x00294679
		public string GetDescription()
		{
			if (this.materialRef == null)
			{
				return this.materialVal.ToString();
			}
			return this.materialRef.Key;
		}

		// Token: 0x04005A82 RID: 23170
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(MaterialVariable)
		})]
		public MaterialVariable materialRef;

		// Token: 0x04005A83 RID: 23171
		[SerializeField]
		public Material materialVal;
	}
}
