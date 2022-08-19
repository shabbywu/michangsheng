using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE8 RID: 3816
	[Serializable]
	public struct SpriteData
	{
		// Token: 0x06006B72 RID: 27506 RVA: 0x002967AA File Offset: 0x002949AA
		public SpriteData(Sprite v)
		{
			this.spriteVal = v;
			this.spriteRef = null;
		}

		// Token: 0x06006B73 RID: 27507 RVA: 0x002967BA File Offset: 0x002949BA
		public static implicit operator Sprite(SpriteData spriteData)
		{
			return spriteData.Value;
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06006B74 RID: 27508 RVA: 0x002967C3 File Offset: 0x002949C3
		// (set) Token: 0x06006B75 RID: 27509 RVA: 0x002967E5 File Offset: 0x002949E5
		public Sprite Value
		{
			get
			{
				if (!(this.spriteRef == null))
				{
					return this.spriteRef.Value;
				}
				return this.spriteVal;
			}
			set
			{
				if (this.spriteRef == null)
				{
					this.spriteVal = value;
					return;
				}
				this.spriteRef.Value = value;
			}
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x00296809 File Offset: 0x00294A09
		public string GetDescription()
		{
			if (this.spriteRef == null)
			{
				return this.spriteVal.ToString();
			}
			return this.spriteRef.Key;
		}

		// Token: 0x04005A8E RID: 23182
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(SpriteVariable)
		})]
		public SpriteVariable spriteRef;

		// Token: 0x04005A8F RID: 23183
		[SerializeField]
		public Sprite spriteVal;
	}
}
