using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED4 RID: 3796
	[Serializable]
	public struct AnimatorData
	{
		// Token: 0x06006B17 RID: 27415 RVA: 0x002959AA File Offset: 0x00293BAA
		public static implicit operator Animator(AnimatorData animatorData)
		{
			return animatorData.Value;
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x002959B3 File Offset: 0x00293BB3
		public AnimatorData(Animator v)
		{
			this.animatorVal = v;
			this.animatorRef = null;
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06006B19 RID: 27417 RVA: 0x002959C3 File Offset: 0x00293BC3
		// (set) Token: 0x06006B1A RID: 27418 RVA: 0x002959E5 File Offset: 0x00293BE5
		public Animator Value
		{
			get
			{
				if (!(this.animatorRef == null))
				{
					return this.animatorRef.Value;
				}
				return this.animatorVal;
			}
			set
			{
				if (this.animatorRef == null)
				{
					this.animatorVal = value;
					return;
				}
				this.animatorRef.Value = value;
			}
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x00295A09 File Offset: 0x00293C09
		public string GetDescription()
		{
			if (this.animatorRef == null)
			{
				return this.animatorVal.ToString();
			}
			return this.animatorRef.Key;
		}

		// Token: 0x04005A66 RID: 23142
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(AnimatorVariable)
		})]
		public AnimatorVariable animatorRef;

		// Token: 0x04005A67 RID: 23143
		[SerializeField]
		public Animator animatorVal;
	}
}
