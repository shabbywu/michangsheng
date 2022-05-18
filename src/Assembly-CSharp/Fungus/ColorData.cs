using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200137B RID: 4987
	[Serializable]
	public struct ColorData
	{
		// Token: 0x060078D2 RID: 30930 RVA: 0x000521E8 File Offset: 0x000503E8
		public ColorData(Color v)
		{
			this.colorVal = v;
			this.colorRef = null;
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x000521F8 File Offset: 0x000503F8
		public static implicit operator Color(ColorData colorData)
		{
			return colorData.Value;
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060078D4 RID: 30932 RVA: 0x00052201 File Offset: 0x00050401
		// (set) Token: 0x060078D5 RID: 30933 RVA: 0x00052223 File Offset: 0x00050423
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

		// Token: 0x060078D6 RID: 30934 RVA: 0x00052247 File Offset: 0x00050447
		public string GetDescription()
		{
			if (this.colorRef == null)
			{
				return this.colorVal.ToString();
			}
			return this.colorRef.Key;
		}

		// Token: 0x040068DB RID: 26843
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(ColorVariable)
		})]
		public ColorVariable colorRef;

		// Token: 0x040068DC RID: 26844
		[SerializeField]
		public Color colorVal;
	}
}
