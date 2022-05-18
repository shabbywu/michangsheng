using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001389 RID: 5001
	[Serializable]
	public struct SpriteData
	{
		// Token: 0x06007911 RID: 30993 RVA: 0x0005279E File Offset: 0x0005099E
		public SpriteData(Sprite v)
		{
			this.spriteVal = v;
			this.spriteRef = null;
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x000527AE File Offset: 0x000509AE
		public static implicit operator Sprite(SpriteData spriteData)
		{
			return spriteData.Value;
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06007913 RID: 30995 RVA: 0x000527B7 File Offset: 0x000509B7
		// (set) Token: 0x06007914 RID: 30996 RVA: 0x000527D9 File Offset: 0x000509D9
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

		// Token: 0x06007915 RID: 30997 RVA: 0x000527FD File Offset: 0x000509FD
		public string GetDescription()
		{
			if (this.spriteRef == null)
			{
				return this.spriteVal.ToString();
			}
			return this.spriteRef.Key;
		}

		// Token: 0x040068F7 RID: 26871
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(SpriteVariable)
		})]
		public SpriteVariable spriteRef;

		// Token: 0x040068F8 RID: 26872
		[SerializeField]
		public Sprite spriteVal;
	}
}
