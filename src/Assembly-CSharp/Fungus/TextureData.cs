using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EED RID: 3821
	[Serializable]
	public struct TextureData
	{
		// Token: 0x06006B89 RID: 27529 RVA: 0x00296AAE File Offset: 0x00294CAE
		public TextureData(Texture v)
		{
			this.textureVal = v;
			this.textureRef = null;
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x00296ABE File Offset: 0x00294CBE
		public static implicit operator Texture(TextureData textureData)
		{
			return textureData.Value;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06006B8B RID: 27531 RVA: 0x00296AC7 File Offset: 0x00294CC7
		// (set) Token: 0x06006B8C RID: 27532 RVA: 0x00296AE9 File Offset: 0x00294CE9
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

		// Token: 0x06006B8D RID: 27533 RVA: 0x00296B0D File Offset: 0x00294D0D
		public string GetDescription()
		{
			if (this.textureRef == null)
			{
				return this.textureVal.ToString();
			}
			return this.textureRef.Key;
		}

		// Token: 0x04005A98 RID: 23192
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(TextureVariable)
		})]
		public TextureVariable textureRef;

		// Token: 0x04005A99 RID: 23193
		[SerializeField]
		public Texture textureVal;
	}
}
