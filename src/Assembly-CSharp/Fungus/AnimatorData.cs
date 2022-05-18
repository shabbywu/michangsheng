using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001375 RID: 4981
	[Serializable]
	public struct AnimatorData
	{
		// Token: 0x060078B6 RID: 30902 RVA: 0x00051F5B File Offset: 0x0005015B
		public static implicit operator Animator(AnimatorData animatorData)
		{
			return animatorData.Value;
		}

		// Token: 0x060078B7 RID: 30903 RVA: 0x00051F64 File Offset: 0x00050164
		public AnimatorData(Animator v)
		{
			this.animatorVal = v;
			this.animatorRef = null;
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060078B8 RID: 30904 RVA: 0x00051F74 File Offset: 0x00050174
		// (set) Token: 0x060078B9 RID: 30905 RVA: 0x00051F96 File Offset: 0x00050196
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

		// Token: 0x060078BA RID: 30906 RVA: 0x00051FBA File Offset: 0x000501BA
		public string GetDescription()
		{
			if (this.animatorRef == null)
			{
				return this.animatorVal.ToString();
			}
			return this.animatorRef.Key;
		}

		// Token: 0x040068CF RID: 26831
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(AnimatorVariable)
		})]
		public AnimatorVariable animatorRef;

		// Token: 0x040068D0 RID: 26832
		[SerializeField]
		public Animator animatorVal;
	}
}
