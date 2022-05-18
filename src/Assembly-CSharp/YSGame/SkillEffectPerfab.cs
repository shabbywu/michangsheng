using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DA4 RID: 3492
	public class SkillEffectPerfab : MonoBehaviour
	{
		// Token: 0x0600544A RID: 21578 RVA: 0x0003C5A2 File Offset: 0x0003A7A2
		private void Start()
		{
			SkillEffectPerfab.inst = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x04005406 RID: 21510
		[SerializeField]
		public List<skillEffctType> EffectsList = new List<skillEffctType>();

		// Token: 0x04005407 RID: 21511
		public static SkillEffectPerfab inst;
	}
}
