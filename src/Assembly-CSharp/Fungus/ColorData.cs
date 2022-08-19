using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EDA RID: 3802
	[Serializable]
	public struct ColorData
	{
		// Token: 0x06006B33 RID: 27443 RVA: 0x00295DCA File Offset: 0x00293FCA
		public ColorData(Color v)
		{
			this.colorVal = v;
			this.colorRef = null;
		}

		// Token: 0x06006B34 RID: 27444 RVA: 0x00295DDA File Offset: 0x00293FDA
		public static implicit operator Color(ColorData colorData)
		{
			return colorData.Value;
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06006B35 RID: 27445 RVA: 0x00295DE3 File Offset: 0x00293FE3
		// (set) Token: 0x06006B36 RID: 27446 RVA: 0x00295E05 File Offset: 0x00294005
		public Color Value
		{
			get
			{
				if (!(this.colorRef == null))
				{
					return this.colorRef.Value;
				}
				return this.colorVal;
			}
			set
			{
				if (this.colorRef == null)
				{
					this.colorVal = value;
					return;
				}
				this.colorRef.Value = value;
			}
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x00295E29 File Offset: 0x00294029
		public string GetDescription()
		{
			if (this.colorRef == null)
			{
				return this.colorVal.ToString();
			}
			return this.colorRef.Key;
		}

		// Token: 0x04005A72 RID: 23154
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(ColorVariable)
		})]
		public ColorVariable colorRef;

		// Token: 0x04005A73 RID: 23155
		[SerializeField]
		public Color colorVal;
	}
}
