using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A75 RID: 2677
	public class SkillEffectPerfab : MonoBehaviour
	{
		// Token: 0x06004B36 RID: 19254 RVA: 0x001FF9EF File Offset: 0x001FDBEF
		private void Start()
		{
			SkillEffectPerfab.inst = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x04004A61 RID: 19041
		[SerializeField]
		public List<skillEffctType> EffectsList = new List<skillEffctType>();

		// Token: 0x04004A62 RID: 19042
		public static SkillEffectPerfab inst;
	}
}
