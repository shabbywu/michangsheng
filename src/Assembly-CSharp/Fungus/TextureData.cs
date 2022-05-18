using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200138E RID: 5006
	[Serializable]
	public struct TextureData
	{
		// Token: 0x06007928 RID: 31016 RVA: 0x000529F0 File Offset: 0x00050BF0
		public TextureData(Texture v)
		{
			this.textureVal = v;
			this.textureRef = null;
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x00052A00 File Offset: 0x00050C00
		public static implicit operator Texture(TextureData textureData)
		{
			return textureData.Value;
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600792A RID: 31018 RVA: 0x00052A09 File Offset: 0x00050C09
		// (set) Token: 0x0600792B RID: 31019 RVA: 0x00052A2B File Offset: 0x00050C2B
		public Texture Value
		{
			get
			{
				if (!(this.textureRef == null))
				{
					return this.textureRef.Value;
				}
				return this.textureVal;
			}
			set
			{
				if (this.textureRef == null)
				{
					this.textureVal = value;
					return;
				}
				this.textureRef.Value = value;
			}
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x00052A4F File Offset: 0x00050C4F
		public string GetDescription()
		{
			if (this.textureRef == null)
			{
				return this.textureVal.ToString();
			}
			return this.textureRef.Key;
		}

		// Token: 0x04006901 RID: 26881
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(TextureVariable)
		})]
		public TextureVariable textureRef;

		// Token: 0x04006902 RID: 26882
		[SerializeField]
		public Texture textureVal;
	}
}
